using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private InventorySlotUI[] _slots;
    public int SlotsAmount => _slots.Length;

    public void SetItemsInSlot(int index, ResourceType type, int amount)
    {
        if (index >= _slots.Length || index < 0)
        {
            return;
        }

        _slots[index].SetItems(type, amount);
    }

    public void RemoveItemsInSlot(int index)
    {
        if (index >= _slots.Length || index < 0)
        {
            return;
        }

        _slots[index].RemoveItem();
    }

    public void UpdateItemsAmount(int index, int amount)
    {
        if (index >= _slots.Length || index < 0)
        {
            return;
        }

        _slots[index].UpdateItemsAmount(amount);
    }
}
