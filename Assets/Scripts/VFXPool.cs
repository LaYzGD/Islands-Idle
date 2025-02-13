using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class VFXPool : MonoBehaviour
{
    private Dictionary<VFXObjectData, ObjectPool<VFXObject>> _pools = new ();

    public void CreatePool(VFXObjectData type)
    {
        _pools.Add(type, new ObjectPool<VFXObject>(() => Instantiate(type.Prefab), (vfx) => vfx.gameObject.SetActive(true), (vfx) => vfx.gameObject.SetActive(false), (vfx) => Destroy(vfx.gameObject), false));
    }

    public void SpawnVFX(VFXObjectData type, Vector3 position, Quaternion rotation)
    {
        if (!_pools.ContainsKey(type)) 
        {
            CreatePool(type);
        }

        var pool = _pools[type];
        var vfx = pool.Get();
        vfx.Init(type, OnVFXStop);
        vfx.transform.position = position;
        vfx.transform.rotation = rotation;
        vfx.Play();
    }

    private void OnVFXStop(VFXObjectData type, VFXObject vfx)
    {
        if (!_pools.ContainsKey(type))
        {
            Destroy(vfx.gameObject);
            return;
        }

        var pool = _pools[type];
        pool.Release(vfx);
    }
}
