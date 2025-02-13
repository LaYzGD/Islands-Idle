using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private AudioPlayer _audioPlayer;
    [SerializeField] private VFXPool _vfxPool;
    [SerializeField] private CharacterCore _character;
    [SerializeField] private ResourceObjectPool _resourceObjectPool;

    private void Awake()
    {
        _audioPlayer.Initialize();
        _resourceObjectPool.Initialize(_audioPlayer, _vfxPool);
        _character.Initialize(_audioPlayer, _vfxPool);

        _resourceObjectPool.SpawnAllObjects();
    }
}
