using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _amountText;

    public void SetItems(ResourceType type, int amount)
    {
        _itemImage.gameObject.SetActive(true);
        _itemImage.sprite = type.Sprite;
        _amountText.text = amount.ToString();
    }

    public void UpdateItemsAmount(int amount)
    {
        _amountText.text = amount.ToString();
    }

    public void RemoveItem()
    {
        _itemImage.gameObject.SetActive(false);
    }
}
