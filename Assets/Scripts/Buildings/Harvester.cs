using UnityEngine;

public class Harvester : MonoBehaviour, ICollector
{
    [SerializeField] private int _damage;
    [SerializeField] private VFXObjectData _hitVFX;

    private VFXPool _pool;
    private InventoryController _inventoryController;
    //private AudioPlayer _audio;

    public void Initialize(/*AudioPlayer audio,*/ VFXPool vFXPool, InventoryController inventoryController)
    {
        //_audio = audio;
        _inventoryController = inventoryController;
        _pool = vFXPool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHittable hittable))
        {
            _pool.SpawnVFX(_hitVFX, other.bounds.center, Quaternion.identity);
            hittable.Hit(_damage);
        }
    }

    public bool Collect(ResourceCollectable collectable)
    {
        return _inventoryController.TryAddItems(collectable.ResourceType, 1);
    }
}
