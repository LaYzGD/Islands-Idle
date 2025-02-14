using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterCore : MonoBehaviour, ICollector
{
    [Header("Data")]
    [SerializeField] private LocomotionData _locomotionData;
    [SerializeField] private CharacterAnimationsData _characterAnimationsData;
    [SerializeField] private CharacterCoreData _coreData;
    [Space]
    [Header("Components")]
    [SerializeField] private CharacterController _characterController;
    [Header("References")]
    [SerializeField] private AnimationEffects _animationEffects;
    [SerializeField] private InstrumentCollisionDetector _instrumentCollisionDetector;
    [SerializeField] private Transform _body;
    [SerializeField] private Animator _animator;

    private InputsHandler _inputsHandler;
    private Locomotion _locomotion;
    private StateMachine _locomotionStateMachine;
    private StateMachine _actionStateMachine;
    private InventoryController _inventoryController;

    public ActionIdle ActionIdle { get; private set; }
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public AttackState AttackState { get; private set; }

    private void Update()
    {
        _locomotionStateMachine.Update();
        _actionStateMachine.Update();
    }

    public void Initialize(InputsHandler inputs, AudioPlayer audioPlayer, VFXPool vfxPool, InventoryController inventoryController)
    {
        _inputsHandler = inputs;
        _locomotion = new Locomotion(_characterController, _body, _locomotionData);
        _inventoryController = inventoryController;
        _locomotionStateMachine = new StateMachine();
        _actionStateMachine = new StateMachine();

        ActionIdle = new ActionIdle(this, _actionStateMachine, _inputsHandler);
        IdleState = new IdleState(this, _locomotionStateMachine, _animator, _characterAnimationsData.IdleTransitionParam, _inputsHandler);
        MoveState = new MoveState(this, _locomotionStateMachine, _animator, _characterAnimationsData.MoveTransitionParam, _inputsHandler, _locomotion);
        AttackState = new AttackState(this, _actionStateMachine, _animator, _characterAnimationsData.AttackLayerName, _characterAnimationsData.AttackTransitionParam, _inputsHandler);
        _animationEffects.Initialize(audioPlayer, vfxPool);
        _instrumentCollisionDetector.Initialize(_coreData.Damage, vfxPool, _coreData.InstrumentHitVFX);
        _locomotionStateMachine.Start(IdleState);
        _actionStateMachine.Start(ActionIdle);
    }

    public bool Collect(ResourceCollectable collectable)
    {
        return _inventoryController.TryAddItems(collectable.ResourceType, 1);
    }
}
