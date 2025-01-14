using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchEnemy : State<Agent>
{
    public override string Name => "Search Enemy";

    private List<Vector2> positionsToGo = new();
    
    public override void Enter(Agent obj)
    {
        Vector2 target;

        while (true)
        {
            var selected =
                Graph.Instance.nodes.ElementAt(Random.Range(0, Graph.Instance.nodes.Count));
            if (Vector2.Distance(selected.Key, obj.Position) > 10)
            {
                target = selected.Key;
                break;
            }
        }

        positionsToGo = Graph.Instance.AStar(obj.Position, target);
    }

    public override void Execute(Agent obj)
    {
        if (obj.SeeEnemy().DoSee)
        {
            obj.GetFSM().ChangeState(new Fight());
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
