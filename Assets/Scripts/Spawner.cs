using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject agentPrefab;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        var agent = Instantiate(agentPrefab, transform.position, Quaternion.identity).GetComponent<Agent>();
        agent.OnDeath += AgentDied;
    }

    private void AgentDied()
    {
        Spawn();
    }
}
