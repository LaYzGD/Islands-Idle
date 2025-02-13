using UnityEngine;

public class MoveState : State
{
    private Animator _animator;
    private string _transitionParam;
    private InputsHandler _inputHandler;
    private Locomotion _locomotion;

    public MoveState(CharacterCore core, StateMachine machine, Animator animator, string transitionParam, InputsHandler inputs, Locomotion locomotion) : base(core, machine)
    {
        _animator = animator;
        _transitionParam = transitionParam;
        _inputHandler = inputs;
        _locomotion = locomotion;
    }

    public override void Enter()
    {
        _animator.SetBool(_transitionParam, true);
    }

    public override void Update()
    {
        var inputVector = _inputHandler.GetInputVector();

        if (inputVector != Vector3.zero)
        {
            _locomotion.Move(inputVector);
            _locomotion.Rotate(inputVector);
            return;
        }

        machine.ChangeState(core.IdleState);
    }

    public override void Exit()
    {
        _animator.SetBool(_transitionParam, false);
    }
}
