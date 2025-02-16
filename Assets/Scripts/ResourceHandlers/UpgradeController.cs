using System;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    private Building[] _forestBuildings;
    private Building[] _caveBuildings;
    private ResourceObjectPool _forestSpawner;
    private ResourceObjectPool _caveSpawner;
    private InventoryController _inventoryController;
    private UpgradeUI _upgradeUI;

    private CraftingRecipe _currentRecipe;
    private UpgradeItemUI _currentUpgradeItem;
    private UpgradeItemUI _nextUpgradeItem;

    public void Initialize(Building[] forestBuildings, Building[] caveBuildings, ResourceObjectPool forestSpawner, ResourceObjectPool caveSpawner, InventoryController inventory, UpgradeUI upgradeUI)
    {
        _forestBuildings = forestBuildings;
        _caveBuildings = caveBuildings;
        _forestSpawner = forestSpawner;
        _caveSpawner = caveSpawner;
        _inventoryController = inventory;
        _upgradeUI = upgradeUI;
        _inventoryController.OnInventoryUpdate += ValidateCraft;
    }

    public void ValidateCraft(CraftingRecipe recipe, UpgradeItemUI upgradeItem, UpgradeItemUI nextUpgradeItem)
    {
        int[] array = new int[recipe.Ingredients.Length];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = _inventoryController.GetItemAmount(recipe.Ingredients[i].ResourceType);
        }

        _upgradeUI.gameObject.SetActive(true);
        _upgradeUI.Set(recipe, array);
        _currentRecipe = recipe;
        _currentUpgradeItem = upgradeItem;
        _nextUpgradeItem = nextUpgradeItem;
    }

    private void ValidateCraft()
    {
        if (!_upgradeUI.gameObject.activeSelf || _currentRecipe == null)
        {
            return;
        }

        int[] array = new int[_currentRecipe.Ingredients.Length];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = _inventoryController.GetItemAmount(_currentRecipe.Ingredients[i].ResourceType);
        }

        _upgradeUI.Set(_currentRecipe, array);
    }

    public void Craft()
    {
        if (_currentRecipe == null)
        {
            return;
        }

        var upgradeInfo = _currentRecipe.UpgradeType;

        UpgradeCaveSoil(upgradeInfo.ResourceObjectData, upgradeInfo.Probability);
        UpgradeForestSoil(upgradeInfo.GrowthTime);
        UpgradeBuildingByZone();

        RemoveItems(_currentRecipe);
        _currentUpgradeItem.SetInteractable(false);
        if (_nextUpgradeItem != null)
        {
            _nextUpgradeItem.SetInteractable(true);
        }
    }

    private void UpgradeBuildingByZone()
    {
        switch (_currentRecipe.UpgradeType.Zone)
        {
            case Zone.None:
                break;
            case Zone.Forest:
                UpgradeBuilding(_currentRecipe.UpgradeType.BuildingLevel, _forestBuildings, _currentRecipe);
                break;
            case Zone.Cave:
                UpgradeBuilding(_currentRecipe.UpgradeType.BuildingLevel, _caveBuildings, _currentRecipe);
                break;
        }
    }

    private void UpgradeForestSoil(float value)
    {
        if (value == 0)
        {
            return;
        }

        _forestSpawner.SetSpawnColdown(value);
    }

    private void RemoveItems(CraftingRecipe recipe)
    {
        foreach (var item in recipe.Ingredients)
        {
            _inventoryController.TryRemoveItems(item.ResourceType, item.Amount);
        }
    }

    private void UpgradeCaveSoil(ResourceObjectData resourceType, float probability)
    {
        if (resourceType == null)
        {
            return;
        }


        _caveSpawner.ChangeSpawnProbability(resourceType, probability);
    }

    private void UpgradeBuilding(int currentIndex, Building[] buildings, CraftingRecipe recipe)
    {
        if (currentIndex >= buildings.Length)
        {
            return;
        }

        if (currentIndex == 0)
        {
            buildings[currentIndex].Enable();
            return;
        }

        buildings[currentIndex - 1].Disable();
        buildings[currentIndex].Enable();
    }

    private void OnDestroy()
    {
        _inventoryController.OnInventoryUpdate -= ValidateCraft;
    }
}

public enum Zone 
{
    None,
    Forest,
    Cave
}

