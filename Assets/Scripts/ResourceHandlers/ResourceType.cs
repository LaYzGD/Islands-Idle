using UnityEngine;

[CreateAssetMenu(menuName = "Data/Resources/Type", fileName = "NewResourceType")]
public class ResourceType : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
}
