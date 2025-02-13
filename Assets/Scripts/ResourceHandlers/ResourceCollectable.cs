using System;
using UnityEngine;

public class ResourceCollectable : MonoBehaviour
{
    [SerializeField] private VFXObjectData _collectVFX;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private float _collectVolume;

    private AudioPlayer _audio;
    private VFXPool _pool;
    private Action<ResourceCollectable> _onCollectAction;

    public void Initialize(AudioPlayer audio, VFXPool pool, Action<ResourceCollectable> onCollectAction)
    {
        _audio = audio;
        _pool = pool;
        _onCollectAction = onCollectAction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollector collector))
        {
            collector.Collect(this);
            _audio.PlaySound(_collectSound, transform, _collectVolume);
            _pool.SpawnVFX(_collectVFX, transform.position, Quaternion.identity);
            _onCollectAction(this);
        }
    }
}
