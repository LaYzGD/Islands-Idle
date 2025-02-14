using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private int _slotCapacity = 99;
    
    private int _inventorySize;

    private InventoryView _inventoryView;
    private InventoryModel _inventory;

    public void Initialize(InventoryView inventoryView)
    {
        _inventoryView = inventoryView;
        _inventorySize = _inventoryView.SlotsAmount;
        _inventory = new InventoryModel(_slotCapacity, _inventorySize);

        _inventory.OnSlotAmountChanged += UpdateView;
        _inventory.OnSlotItemChanged += AddItemToView;
    }

    public bool TryAddItems(ResourceType type, int amount)
    {
        return _inventory.TryAddItems(type, amount);
    }

    public bool TryRemoveItems(ResourceType type, int amount)
    {
        return _inventory.TryRemoveItems(type, amount);
    }

    private void UpdateView(int slotIndex, int amount)
    {
        if (amount == 0)
        {
            _inventoryView.RemoveItemsInSlot(slotIndex);
            return;
        }

        _inventoryView.UpdateItemsAmount(slotIndex, amount);
    }

    private void AddItemToView(int slotIndex, ResourceType type, int amount)
    {
        _inventoryView.SetItemsInSlot(slotIndex, type, amount);
    }
}
