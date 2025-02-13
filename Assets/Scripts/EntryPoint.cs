using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private AudioPlayer _audioPlayer;
    [SerializeField] private VFXPool _vfxPool;
    [SerializeField] private CharacterCore _character;
    [SerializeField] private ResourceObjectPool _resourceObjectPool;
    [SerializeField] private CollectablePool _collectablePool;
    [SerializeField] private Harvester _harvester;

    private void Awake()
    {
        _audioPlayer.Initialize();
        _collectablePool.Initialize(_audioPlayer, _vfxPool);
        _harvester.Initialize(_vfxPool);
        _resourceObjectPool.Initialize(_audioPlayer, _vfxPool, _collectablePool);
        _character.Initialize(_audioPlayer, _vfxPool);

        _resourceObjectPool.SpawnAllObjects();
    }
}
