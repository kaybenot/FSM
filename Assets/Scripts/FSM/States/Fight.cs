using System.Linq;
using UnityEngine;

public class Fight : State<Agent>
{
    public override string Name => "Fight";
    
    public override void Enter(Agent obj)
    {
    }

    public override void Execute(Agent obj)
    {
        var enemy = obj.SeeEnemy();

        if (enemy.DoSee)
        {
            if (obj.Health <= obj.LowHealth)
            {
                obj.GetFSM().ChangeState(new Flee(FindFleePosition(enemy.Agent)));
                return;
            }
            if (obj.Ammo <= obj.LowAmmo)
            {
                obj.GetFSM().ChangeState(new Flee(FindFleePosition(enemy.Agent)));
                return;
            }
            
            obj.Shoot(enemy.Agent);
        }
        else
        {
            obj.GetFSM().ChangeState(new SearchEnemy());
        }
    }

    public override void Exit(Agent obj)
    {
    }

    private Vector2 FindFleePosition(Agent enemy)
    {
        Vector2 target;

        while (true)
        {
            var selected =
                Graph.Instance.nodes.ElementAt(Random.Range(0, Graph.Instance.nodes.Count));
            if (Physics2D.Raycast(selected.Key, (enemy.Position - selected.Key).normalized,
                    (enemy.Position - selected.Key).magnitude - 0.6f))
            {
                return selected.Key;
            }
        }
    }
}
