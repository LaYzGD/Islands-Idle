using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private ItemDataBase _itemDatabase;
    [Space]
    [SerializeField] private GameObject _escapeMenu;
    [SerializeField] private string _menuName = "Menu";

    private SaveAndLoad _saveAndLoad;

    private void Awake()
    {
        _saveAndLoad = SaveAndLoad.Instance;

        _audioPlayer.Initialize();
        _inventoryController.Initialize(_inventoryView, _itemDatabase);
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
        _inputsHandler.OnEscapePressed += ShowEscapeMenu;

        if (_saveAndLoad != null)
        {
            _saveAndLoad.Bind<Building, BuildingData>(_saveAndLoad.GameData.BuildingData);
            _saveAndLoad.Bind<ResourceObjectPool, SpawnData>(_saveAndLoad.GameData.SpawnData);
            _saveAndLoad.Bind<Bridge, BridgeData>(_saveAndLoad.GameData.BridgeData);
            _saveAndLoad.Bind<UpgradeItemUI, UpgradeData>(_saveAndLoad.GameData.UpgradeData);
            _saveAndLoad.Bind<InventoryController, InventoryData>(_saveAndLoad.GameData.InventoryData);
        }
    }

    private void Start()
    {
        _inventoryView.gameObject.SetActive(false);
    }

    private void ShowEscapeMenu()
    {
        Time.timeScale = 0f;
        _escapeMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _escapeMenu.SetActive(false);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        _saveAndLoad.SaveGame();
        SceneManager.LoadScene(_menuName);
    }

    private void OnDisable()
    {
        _inputsHandler.OnEscapePressed -= ShowEscapeMenu;
    }
}
