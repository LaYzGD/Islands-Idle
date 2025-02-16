using System;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class UpgradeItemUI : MonoBehaviour, IBind<UpgradeData>
{
    [SerializeField] private CraftingRecipe _recipe;
    [SerializeField] private UpgradeItemUI _nextUpgrade;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private UpgradeController _upgradeController;
    [SerializeField] private GameObject _tick; 
    [SerializeField] private string _ID;

    public string ID { get => _ID; set => _ID = value; }
    private UpgradeData _upgradeData;

    private void Awake()
    {
        _upgradeImage.sprite = _recipe.Sprite;
    }

    public void Set()
    {
        _upgradeController.ValidateCraft(_recipe, this, _nextUpgrade);
    }

    public void SetInteractable(bool flag)
    {
        _upgradeButton.interactable = flag;
        _tick.SetActive(!flag);
        _upgradeData.IsPurchased = !flag;
        _upgradeData.IsUnlocked = flag;
    }

    public void Bind(UpgradeData data)
    {
        _upgradeData = data;

        if (data.ID == null || data.ID == string.Empty)
        {
            _upgradeData.ID = ID;
            _upgradeData.IsPurchased = false;
            _upgradeData.IsUnlocked = _upgradeButton.interactable;
        }

        if (data.IsPurchased)
        {
            SetInteractable(false);
        }

        if (data.IsUnlocked)
        {
            _upgradeButton.interactable = true;
        }
    }
}

[Serializable]
public class UpgradeData : ISaveable
{
    public string ID { get => ObjectID; set => ObjectID = value; }
    public string ObjectID;
    public bool IsPurchased;
    public bool IsUnlocked;
}