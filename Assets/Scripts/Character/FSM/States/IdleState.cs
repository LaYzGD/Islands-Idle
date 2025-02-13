using UnityEngine;

public class IdleState : State
{
    private Animator _animator;
    private string _transitionParam;
    private InputsHandler _inputHandler;

    public IdleState(CharacterCore core, StateMachine machine, Animator animator, string transitionParam, InputsHandler inputsHandler) : base(core, machine)
    {
        _animator = animator;
        _transitionParam = transitionParam;
        _inputHandler = inputsHandler;
    }

    public override void Enter()
    {
        _animator.SetBool(_transitionParam, true);
    }

    public override void Update()
    {
        if (_inputHandler.GetInputVector() == Vector3.zero)
        {
            return;
        }

        machine.ChangeState(core.MoveState);
    }

    public override void Exit()
    {
        _animator.SetBool(_transitionParam, false);
    }
}
