using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour
{
    [SerializeField] private CraftingRecipe _recipe;
    [SerializeField] private UpgradeItemUI _nextUpgrade;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private UpgradeController _upgradeController;

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
    }
}