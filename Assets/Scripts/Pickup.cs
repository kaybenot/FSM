using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected float respawnTime = 10f;
    [SerializeField] protected GameObject pickupVisual;

    private float pickUpTime;
    private bool active;

    protected virtual void Awake()
    {
        pickupVisual.SetActive(true);
        active = true;
    }

    protected virtual void Update()
    {
        if (pickUpTime < Time.time - respawnTime)
        {
            pickupVisual.SetActive(true);
            active = true;
        }

        if (!active)
        {
            return;
        }

        foreach (var agent in World.Instance.Agents)
        {
            if (Vector2.Distance(agent.Position, transform.position) < 0.25f)
            {
                OnPickedUp(agent);
            }
        }
    }

    public virtual void OnPickedUp(Agent agent)
    {
        pickupVisual.SetActive(false);
        active = false;
        pickUpTime = Time.time;
    }
}
