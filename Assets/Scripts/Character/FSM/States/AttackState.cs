using UnityEngine;

public class AttackState : State
{
    private Animator _animator;
    private string _layerName;
    private string _transitionParam;
    private InputsHandler _inputsHandler;


    public AttackState(CharacterCore core, StateMachine machine, Animator animator, string layerName, string tranitionParam, InputsHandler inputsHandler) : base(core, machine)
    {
        _animator = animator;
        _layerName = layerName;
        _transitionParam = tranitionParam;
        _inputsHandler = inputsHandler;
    }

    public override void Enter()
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex(_layerName), 1);
        _animator.SetBool(_transitionParam, true);
    }

    public override void Update()
    {
        if (_inputsHandler.IsMousePressed)
        {
            return;
        }

        machine.ChangeState(core.ActionIdle);
    }

    public override void Exit()
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex(_layerName), 0);
        _animator.SetBool(_transitionParam, false);
    }
}
