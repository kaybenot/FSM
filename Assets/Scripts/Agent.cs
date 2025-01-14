using System;
using Unity.VisualScripting;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameObject projectilePrefab;

    [Header("Debug")]
    [SerializeField] private string currentState = "No state";

    [field: SerializeField] public int Health { get; set; } = 10;
    [field: SerializeField] public int Ammo { get; set; } = 10;
    [field: SerializeField] public int Armor { get; set; } = 0;
    [field: SerializeField] public float Speed { get; set; } = 1f;
    [field: SerializeField] public float FireRate { get; set; } = 1f;

    public int LowHealth => 4;
    public int LowAmmo => 0;

    public Vector2 Position => transform.position;

    public Action<Agent> OnDeath { get; set; }
    
    private FSM<Agent> fsm;
    private Vector2 moveGoal;
    private float lastShootTime;
    
    public FSM<Agent> GetFSM()
    {
        return fsm;
    }

    private void Awake()
    {
        fsm = new FSM<Agent>(this, new SearchEnemy());
        currentState = fsm.GetStateName();
        moveGoal = Position;
    }

    private void Update()
    {
        fsm.Update();
        currentState = fsm.GetStateName();
        
        var direction = (moveGoal - Position).normalized;
        var distance = (moveGoal - Position).magnitude;
        var step = direction * Speed * Time.deltaTime;
        if (distance < step.magnitude)
        {
            step = step.normalized * distance;
        }
        transform.position += (Vector3)step;
    }

    public void MoveTo(Vector2 position)
    {
        moveGoal = position;
    }

    public (bool DoSee, Agent Agent) SeeEnemy()
    {
        foreach (var agent in World.Instance.Agents)
        {
            if (agent == this)
            {
                continue;
            }

            var direction = (agent.Position - Position).normalized;
            var distance = (agent.Position - Position).magnitude;

            if (!Physics2D.Raycast(Position, direction, distance))
            {
                return (true, agent);
            }
        }
        return (false, null);
    }

    public void Shoot(Agent agent)
    {
        if (lastShootTime + 1f / FireRate > Time.time)
        {
            return;
        }

        lastShootTime = Time.time;
        
        var direction = (agent.Position - Position).normalized;
        var speed = 20f;

        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.SetState(speed, direction, agent);
    }

    public void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public void Damage(int dmg)
    {
        if (Armor > 0)
        {
            Armor -= dmg;
            if (Armor < 0)
            {
                Health += Armor;
                if (Health <= 0)
                {
                    Die();
                }
                Armor = 0;
            }

            return;
        }
        
        Health -= dmg;
        if (Health <= 0)
        {
            Die();
        }
    }
}