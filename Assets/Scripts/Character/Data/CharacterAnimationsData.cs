using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character/Animations", fileName = "NewCharacterAnimationsData")]
public class CharacterAnimationsData : ScriptableObject
{
    [field: SerializeField] public string IdleTransitionParam { get; private set; } = "Idle";
    [field: SerializeField] public string MoveTransitionParam { get; private set; } = "Move";
    [field: SerializeField] public string AttackTransitionParam { get; private set; } = "Attack";
    [field: SerializeField] public string AttackLayerName { get; private set; } = "Attack Layer";
}
