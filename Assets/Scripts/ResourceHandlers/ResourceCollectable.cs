using System;
using UnityEngine;

public class ResourceCollectable : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private VFXObjectData _collectVFX;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private float _collectVolume;
    public ResourceType ResourceType { get; private set; }

    private AudioPlayer _audio;
    private VFXPool _pool;
    private Action<ResourceCollectable> _onCollectAction;

    public void Initialize(ResourceType type, AudioPlayer audio, VFXPool pool, Action<ResourceCollectable> onCollectAction)
    {
        ResourceType = type;
        _audio = audio;
        _pool = pool;
        _onCollectAction = onCollectAction;
        _meshFilter.sharedMesh = type.Mesh;
        _meshRenderer.sharedMaterials = type.Materials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollector collector))
        {
            if (collector.Collect(this)) 
            {
                _audio.PlaySound(_collectSound, transform, _collectVolume);
                _pool.SpawnVFX(_collectVFX, transform.position, Quaternion.identity);
                _onCollectAction(this);
            }
        }
    }
}
