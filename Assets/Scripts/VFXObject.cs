using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class VFXObject : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private Action<VFXObjectData, VFXObject> _action;
    public VFXObjectData Type { get; private set; }
    public void Init(VFXObjectData type, Action<VFXObjectData, VFXObject> action)
    {
        _action = action;
        Type = type;
    }

    public void Play()
    {
        _particleSystem.Play();
    }

    private void OnParticleSystemStopped()
    {
        _action(Type, this);
    }
}
