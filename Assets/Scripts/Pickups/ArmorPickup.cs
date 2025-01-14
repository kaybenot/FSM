using UnityEngine;

public class ArmorPickup : Pickup
{
    public override void OnPickedUp(Agent agent)
    {
        base.OnPickedUp(agent);

        agent.Armor = 5;
    }
}
