using UnityEngine;

public class ActionIdle : State
{
    private InputsHandler _inputsHandler;
    private CharacterCore _core;

    public ActionIdle(CharacterCore core, StateMachine machine, InputsHandler inputsHandler) : base(core, machine) 
    {
        _inputsHandler = inputsHandler;
        _core = core;
    }

    public override void Enter() { }

    public override void Update()
    {
        if (!_inputsHandler.IsMousePressed)
        {
            return;
        }

        machine.ChangeState(core.AttackState);
    }

    public override void Exit() { }
}
