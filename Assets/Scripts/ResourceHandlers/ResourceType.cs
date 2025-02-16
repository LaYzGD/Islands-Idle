using UnityEngine;

[CreateAssetMenu(menuName = "Data/Resources/Type", fileName = "NewResourceType")]
public class ResourceType : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Mesh Mesh { get; private set; }
    [field: SerializeField] public Material[] Materials { get; private set; }
}
