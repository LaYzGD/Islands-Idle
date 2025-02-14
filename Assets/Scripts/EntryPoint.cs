using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private InputsHandler _inputsHandler;
    [SerializeField] private AudioPlayer _audioPlayer;
    [SerializeField] private VFXPool _vfxPool;
    [SerializeField] private CharacterCore _character;
    [SerializeField] private ResourceObjectPool _resourceObjectPool;
    [SerializeField] private CollectablePool _collectablePool;
    [SerializeField] private Building _building;
    [Space]
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private InventoryController _inventoryController;

    private void Awake()
    {
        _audioPlayer.Initialize();
        _inventoryController.Initialize(_inventoryView);
        _collectablePool.Initialize(_audioPlayer, _vfxPool);
        _building.Initialize(_vfxPool, _inventoryController);
        _resourceObjectPool.Initialize(_audioPlayer, _vfxPool, _collectablePool);
        _character.Initialize(_inputsHandler, _audioPlayer, _vfxPool, _inventoryController);

        _resourceObjectPool.SpawnAllObjects();
    }
}
