using UnityEngine;

[CreateAssetMenu(menuName = "Data/Resources/ResourceObject", fileName = "NewResourceObjectData")]
public class ResourceObjectData : ScriptableObject
{
    [field: SerializeField] public VFXObjectData HitVFX { get; private set; }
    [field: SerializeField] public AudioClip HitAudio { get; private set; }
    [field: SerializeField] public int Durability { get; private set; }
    [field: SerializeField] public ResourceType ResourceType { get; private set; }
}
