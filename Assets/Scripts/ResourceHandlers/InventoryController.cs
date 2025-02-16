using System;
using UnityEngine;

public class InventoryController : MonoBehaviour, IBind<InventoryData>
{
    [SerializeField] private int _slotCapacity = 99;
    
    private int _inventorySize;

    private InventoryView _inventoryView;
    private InventoryModel _inventory;
    private ItemDataBase _itemDatabase;

    private InventoryData _inventoryData;
    public InventoryData InventoryData => _inventoryData;

    public event Action OnInventoryUpdate;

    public string ID { get; set; }

    public void Initialize(InventoryView inventoryView, ItemDataBase database)
    {
        _inventoryView = inventoryView;
        _inventorySize = _inventoryView.SlotsAmount;
        _inventory = new InventoryModel(_slotCapacity, _inventorySize);
        _itemDatabase = database;

        _inventory.OnSlotAmountChanged += UpdateView;
        _inventory.OnSlotItemChanged += AddItemToView;
    }

    public int GetItemAmount(ResourceType type)
    {
        return _inventory.GetItemsAmount(type);
    }

    public bool TryAddItems(ResourceType type, int amount)
    {
        bool result = _inventory.TryAddItems(type, amount);
        if (result)
        {
           _inventoryData.Items = _inventory.GetItems().ToArray();
            OnInventoryUpdate?.Invoke();
        }
        return result;
    }

    public bool TryRemoveItems(ResourceType type, int amount)
    {
        bool result = _inventory.TryRemoveItems(type, amount);
        if (result)
        {
            _inventoryData.Items = _inventory.GetItems().ToArray();
            OnInventoryUpdate?.Invoke();
        }
        return result;
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

    void IBind<InventoryData>.Bind(InventoryData data)
    {
        ID = data.ID;
        _inventoryData = data;

        if (ID == null || ID == string.Empty)
        {
            ID = $"P1{UnityEngine.Random.value}P2{UnityEngine.Random.value}P3{UnityEngine.Random.value}P4{UnityEngine.Random.value}P5{UnityEngine.Random.value}";
            _inventoryData.ID = ID;
        }

        if (data.Items == null)
        {
            data.Items = new Item[_inventorySize];
            return;
        }

        foreach (var item in data.Items)
        {
            var resource = _itemDatabase.GetItemByName(item.Name);
            TryAddItems(resource, item.Amount);
        }
    }
}

[Serializable]
public class InventoryData : ISaveable 
{
    public string ID { get => ObjectID; set => ObjectID = value; }
    public string ObjectID;
    public Item[] Items;
}

[Serializable]
public class Item
{
    public string Name;
    public int Amount;

    public Item(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }
}
