using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character/Locomotion", fileName = "NewLocomotionData")]
public class LocomotionData : ScriptableObject
{
    [field: SerializeField] public float MovementSpeed { get; private set; } = 6f;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 360f;
}
