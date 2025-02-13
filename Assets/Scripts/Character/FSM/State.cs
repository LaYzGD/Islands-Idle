public abstract class State
{
    protected CharacterCore core { get; private set; }
    protected StateMachine machine { get; private set; }

    public State(CharacterCore core, StateMachine machine)
    {
        this.core = core;
        this.machine = machine;
    }

    public abstract void Enter();
    public virtual void Update() { }
    public abstract void Exit();
}
