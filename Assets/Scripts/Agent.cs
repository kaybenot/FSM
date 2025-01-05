using System;
using Unity.VisualScripting;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private string currentState = "No state";
    
    private FSM<Agent> fsm;
    
    public FSM<Agent> GetFSM()
    {
        return fsm;
    }

    private void Awake()
    {
        fsm = new FSM<Agent>(this, new TestState());
        currentState = fsm.GetStateName();
    }

    private void Update()
    {
        fsm.Update();
        currentState = fsm.GetStateName();
    }
}