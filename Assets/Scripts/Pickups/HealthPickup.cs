using UnityEngine;

public class HealthPickup : Pickup
{
    public override void OnPickedUp(Agent agent)
    {
        base.OnPickedUp(agent);

        agent.Health = 10;
    }
}
