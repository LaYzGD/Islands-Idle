using UnityEngine;

public class Harvester : MonoBehaviour, ICollector
{
    [SerializeField] private int _damage;
    [SerializeField] private VFXObjectData _hitVFX;

    private VFXPool _pool;
    //private AudioPlayer _audio;

    public void Initialize(/*AudioPlayer audio,*/ VFXPool vFXPool)
    {
        //_audio = audio;
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

    public void Collect(ResourceCollectable collectable)
    {
        print("Collect");
    }
}
