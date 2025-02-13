public class StateMachine
{
    private State _currentState;

    public void Start(State startingState)
    {
        _currentState = startingState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void ChangeState(State newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}

public enum StateEnum
{
    Idle,
    Move,
    ActionIdle,
    Attack
}
