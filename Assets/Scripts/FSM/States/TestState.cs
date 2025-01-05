using UnityEngine;

public class TestState : State<Agent>
{
    private float timer;
    private bool executed = false;

    public override string Name => "TestState";

    public override void Enter(Agent obj)
    {
        Debug.Log($"Entered TestState for object {obj.name}");

        timer = Time.time;
    }

    public override void Execute(Agent obj)
    {
        if (!executed)
        {
            Debug.Log($"Started executing TestState for object {obj.name}");
            executed = true;
        }

        // Change to next TestState after 5 seconds
        if (Time.time - timer > 5f)
        {
            obj.GetFSM().ChangeState(new TestState());
        }
    }

    public override void Exit(Agent obj)
    {
        Debug.Log($"Exited TestState for object {obj.name}");
    }
}
