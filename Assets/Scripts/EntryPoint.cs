using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private InputsHandler _inputsHandler;
    [SerializeField] private AudioPlayer _audioPlayer;
    [SerializeField] private VFXPool _vfxPool;
    [SerializeField] private CharacterCore _character;
    [Space]
    [SerializeField] private ResourceObjectPool _forestResourceObjectPool;
    [SerializeField] private ResourceObjectPool _caveResourceObjectPool;
    [Space]
    [SerializeField] private CollectablePool _collectablePool;
    [Space]
    [SerializeField] private Building[] _forrestBuildings;
    [SerializeField] private Building[] _caveBuildings;
    [Space]
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private InventoryController _inventoryController;
    [SerializeField] private UpgradeUI _upgradeUI;
    [SerializeField] private UpgradeController _upgradeController;
    [SerializeField] private BridgeOpenUI[] _bridgesOpeners;

    private void Awake()
    {
        _audioPlayer.Initialize();
        _inventoryController.Initialize(_inventoryView);
        _collectablePool.Initialize(_audioPlayer, _vfxPool);
        foreach (var building in _forrestBuildings)
        {
            building.Initialize(_vfxPool, _inventoryController);
        }

        foreach (var building in _caveBuildings)
        {
            building.Initialize(_vfxPool, _inventoryController);
        }

        _forestResourceObjectPool.Initialize(_audioPlayer, _vfxPool, _collectablePool);
        _caveResourceObjectPool.Initialize(_audioPlayer, _vfxPool, _collectablePool);
        _character.Initialize(_inputsHandler, _audioPlayer, _vfxPool, _inventoryController);
        _upgradeController.Initialize(_forrestBuildings, _caveBuildings, _forestResourceObjectPool, _caveResourceObjectPool, _inventoryController, _upgradeUI);
        foreach (var item in _bridgesOpeners)
        {
            item.Initialize(_inventoryController);
        }

        _forestResourceObjectPool.SpawnAllObjects();
        _caveResourceObjectPool.SpawnAllObjects();
    }
}
