using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flee : State<Agent>
{
    public override string Name { get; } = "Flee";
    
    private List<Vector2> positionsToGo = new();

    private readonly Vector2 fleePos;

    public Flee(Vector2 fleePos)
    {
        this.fleePos = fleePos;
    }
    
    public override void Enter(Agent obj)
    {
        positionsToGo = Graph.Instance.AStar(obj.Position, fleePos);
    }

    public override void Execute(Agent obj)
    {
        var enemy = obj.SeeEnemy();

        if (!enemy.DoSee && obj.Health <= obj.LowHealth)
        {
            obj.GetFSM().ChangeState(new PickupHealth());
            return;
        }
        if (!enemy.DoSee && obj.Ammo <= obj.LowAmmo)
        {
            obj.GetFSM().ChangeState(new PickupAmmo());
            return;
        }
        else if (!enemy.DoSee)
        {
            obj.GetFSM().ChangeState(new PickupArmor());
            return;
        }

        if (positionsToGo.Count <= 0)
        {
            obj.GetFSM().ChangeState(new SearchEnemy());
            return;
        }
        
        var target = positionsToGo[0];
        
        obj.MoveTo(target);

        if (Vector2.Distance(target, obj.Position) < 1e-4)
        {
            positionsToGo.RemoveAt(0);
        }
    }

    public override void Exit(Agent obj)
    {
    }
}
