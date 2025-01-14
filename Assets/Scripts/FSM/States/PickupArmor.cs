using System.Collections.Generic;
using UnityEngine;

public class PickupArmor : State<Agent>
{
    public override string Name => "Pickup Armor";
    
    private List<Vector2> positionsToGo = new();
    
    public override void Enter(Agent obj)
    {
        var pickups = Object.FindObjectsByType<HealthPickup>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        if (pickups.Length <= 0)
        {
            obj.GetFSM().ChangeState(new SearchEnemy());
            return;
        }

        var closest = pickups[0];
        foreach (var pickup in pickups)
        {
            if (Vector2.Distance(obj.Position, pickup.transform.position) <
                Vector2.Distance(obj.Position, closest.transform.position))
            {
                closest = pickup;
            }
        }

        positionsToGo = Graph.Instance.AStar(obj.Position, closest.transform.position);
        positionsToGo.Add(closest.transform.position);
    }

    public override void Execute(Agent obj)
    {
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
