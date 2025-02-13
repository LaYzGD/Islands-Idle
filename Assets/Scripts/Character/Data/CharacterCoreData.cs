using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character/Core", fileName = "NewCoreData")]
public class CharacterCoreData : ScriptableObject
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public VFXObjectData InstrumentHitVFX { get; private set; }
}
