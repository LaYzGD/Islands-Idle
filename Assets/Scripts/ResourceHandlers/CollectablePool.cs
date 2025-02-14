using UnityEngine;
using UnityEngine.Pool;

public class CollectablePool : MonoBehaviour
{
    [SerializeField] private ResourceCollectable _resource;
    private ObjectPool<ResourceCollectable> _pool;

    private AudioPlayer _audioPlayer;
    private VFXPool _vfxPool;

    public void Initialize(AudioPlayer audio, VFXPool pool)
    {
        _pool = new ObjectPool<ResourceCollectable>(() => Instantiate(_resource), (obj) => obj.gameObject.SetActive(true), (obj) => obj.gameObject.SetActive(false), (obj) => Destroy(obj.gameObject), false);
        _audioPlayer = audio;
        _vfxPool = pool;
    }

    public void SpawnItem(Vector3 position, ResourceType type)
    {
        var item = _pool.Get();
        item.transform.position = position;
        item.Initialize(type, _audioPlayer, _vfxPool, OnItemCollect);
    }

    private void OnItemCollect(ResourceCollectable item) 
    {
        _pool.Release(item);
    }
}
