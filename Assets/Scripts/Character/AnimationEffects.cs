using UnityEngine;

public class AnimationEffects : MonoBehaviour
{
    [SerializeField] private AudioClip[] _stepSounds;

    [SerializeField] private float _volume;
    
    [SerializeField] private Transform _vfxSpawnPoint;

    [SerializeField] private Transform _vfxSpawnPointLegs;

    private VFXPool _pool;
    private AudioPlayer _audioPlayer;

    public void Initialize(AudioPlayer audio, VFXPool pool)
    {
        _audioPlayer = audio;
        _pool = pool;
    }

    public void PlaySound(AudioClip sound)
    {
        _audioPlayer.PlaySound(sound, _vfxSpawnPoint, _volume);
    }

    public void PlayStepSounds()
    {
        _audioPlayer.PlaySound(_stepSounds[Random.Range(0, _stepSounds.Length)], _vfxSpawnPointLegs, _volume);
    }

    public void CreateParticle(VFXObjectData vfxData)
    {
        _pool.SpawnVFX(vfxData, _vfxSpawnPoint.position, _vfxSpawnPoint.rotation);
    }

    public void CreateStepParticle(VFXObjectData vfxData)
    {
        _pool.SpawnVFX(vfxData, _vfxSpawnPointLegs.position, Quaternion.identity);
    }
}
