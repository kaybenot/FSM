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

    public Vector2 Position => new Vector2(transform.position.x, transform.position.z);

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
        transform.position += new Vector3(direction.x, 0f, direction.y) * Speed * Time.deltaTime;
    }

    public void MoveTo(Vector2 position)
    {
        moveGoal = position;
    }
}