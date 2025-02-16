using System.Collections.Generic;
using UnityEngine;

public class InventoryModel
{
    private int _defaultSlotCapacity;
    private int _inventoryCapacity;

    public event System.Action<int, int> OnSlotAmountChanged;
    public event System.Action<int, ResourceType, int> OnSlotItemChanged;

    private Dictionary<int, InventorySlot<ResourceType>> _inventory;

    public InventoryModel(int defaultSlotCapacity, int inventoryCapacity)
    {
        _defaultSlotCapacity = defaultSlotCapacity;
        _inventoryCapacity = inventoryCapacity;

        CreateInventory();
    }

    private void CreateInventory()
    {
        _inventory = new Dictionary<int, InventorySlot<ResourceType>>();

        for (int i = 0; i < _inventoryCapacity; i++)
        {
            _inventory.Add(i, new InventorySlot<ResourceType>(default, 0, _defaultSlotCapacity));
        }
    }

    public bool TryAddItems(ResourceType type, int amount)
    {
        int remainder = amount;

        for (int i = 0; i < _inventory.Values.Count; i++)
        {
            var item = _inventory[i];

            if (item.Item == null)
            {
                item.Set(type);

                item.AddItems(remainder, out remainder);

                OnSlotItemChanged?.Invoke(i, item.Item, item.ItemsAmount);

                if (remainder == 0)
                {
                    return true;
                }

                continue;
            }

            if (item.Item != type)
            {
                continue;
            }

            item.AddItems(remainder, out remainder);

            OnSlotAmountChanged?.Invoke(i, item.ItemsAmount);

            if (remainder == 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool TryRemoveItems(ResourceType type, int amount)
    {
        int remainder = amount;

        for (int i = _inventory.Values.Count - 1; i >= 0 ; i--)
        {
            if (_inventory[i].Item == null || _inventory[i].Item != type)
            {
                continue;
            }

            _inventory[i].RemoveItems(remainder, out remainder);
            OnSlotAmountChanged?.Invoke(i, _inventory[i].ItemsAmount);

            if (remainder == 0)
            {
                return true;
            }
        }

        return false;
    }

    public int GetItemsAmount(ResourceType type)
    {
        int amount = 0;

        foreach (var item in _inventory.Values) 
        {
            if (item.Item != type)
            {
                continue;
            }

            amount += item.ItemsAmount;
        }

        return amount;
    }

    public List<Item> GetItems()
    {
        List<Item> list = new List<Item>();
        foreach (var slot in _inventory.Values)
        {
            if (slot.Item == null)
            {
                continue;
            }

            list.Add(new Item(slot.Item.Name, slot.ItemsAmount));
        }

        return list;
    }
}

public class InventorySlot<T>
{
    public T Item { get; private set; }
    public int ItemsAmount { get; private set; }
    public int Capacity { get; private set; }

    public InventorySlot(T item, int itemsAmount, int capacity) 
    {
        Item = item;
        if (itemsAmount <= 0)
        {
            itemsAmount = 0;
            Item = default;
        }

        if (itemsAmount > capacity)
        {
            itemsAmount = capacity;
        }

        ItemsAmount = itemsAmount;
        Capacity = capacity;
    }

    public void Set(T item)
    {
        Item = item;
        ItemsAmount = 1;
    }

    public void AddItems(int amount, out int remainder)
    {
        if (ItemsAmount + amount > Capacity)
        {
            int returnValue = ItemsAmount + amount - Capacity;
            ItemsAmount = Capacity;
            remainder = returnValue;
            return;
        }

        ItemsAmount += amount;
        remainder = 0;
    }

    public void RemoveItems(int amount, out int remainder) 
    {
        if (ItemsAmount <= 0)
        {
            remainder = amount;
            return;
        }

        if (ItemsAmount - amount < 0)
        {
            var returnValue = amount - ItemsAmount;
            ItemsAmount = 0;
            Item = default;
            remainder = returnValue;
            return;
        }

        ItemsAmount -= amount;
        if (ItemsAmount == 0)
        {
            Item = default;
        }

        remainder = 0;
    }
}
