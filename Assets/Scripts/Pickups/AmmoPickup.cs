using UnityEngine;

public class AmmoPickup : Pickup
{
    public override void OnPickedUp(Agent agent)
    {
        base.OnPickedUp(agent);

        agent.Ammo = 10;
    }
}
