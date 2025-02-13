using UnityEngine;

public class InstrumentCollisionDetector : MonoBehaviour
{
    private int _damage;
    private VFXPool _pool;
    private VFXObjectData _vfxData;

    public void Initialize(int damage, VFXPool pool, VFXObjectData vfxData)
    {
        _damage = damage;
        _pool = pool;
        _vfxData = vfxData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHittable hittable)) 
        {
            _pool.SpawnVFX(_vfxData, other.bounds.center, Quaternion.identity);
            hittable.Hit(_damage);
        }
    }
}
