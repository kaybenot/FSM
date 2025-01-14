using System;
using Unity.VisualScripting;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private string currentState = "No state";

    [field: SerializeField] public int Health { get; set; } = 10;
    [field: SerializeField] public int Ammo { get; set; } = 10;
    [field: SerializeField] public int Armor { get; set; } = 0;
    [field: SerializeField] public float Speed { get; set; } = 1f;

    public Vector2 Position => transform.position;

    public Action OnDeath { get; set; }
    
    private FSM<Agent> fsm;
    private Vector2 moveGoal;
    
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
}