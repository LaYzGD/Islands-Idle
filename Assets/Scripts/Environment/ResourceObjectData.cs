using UnityEngine;

[CreateAssetMenu(menuName = "Data/Resources/ResourceObject", fileName = "NewResourceObjectData")]
public class ResourceObjectData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public VFXObjectData HitVFX { get; private set; }
    [field: SerializeField] public AudioClip HitAudio { get; private set; }
    [field: SerializeField] public AudioClip DestroyAudio { get; private set; }
    [field: SerializeField] public int Durability { get; private set; }
    [field: SerializeField] public ResourceType ResourceType { get; private set; }
    [field: SerializeField] public AnimatorOverrideController AnimatorOverrideController { get; private set;}
    [field: SerializeField] public Mesh Mesh { get; private set; }
    [field: SerializeField] public Material[] Materials { get; private set; }
}
