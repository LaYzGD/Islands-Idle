using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Crafting/Recipe", fileName = "NewCraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Ingredient[] Ingredients { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public UpgradeType UpgradeType { get; private set; }
}

[Serializable]
public struct Ingredient 
{
    public ResourceType ResourceType;
    public int Amount;
}

[Serializable]
public struct UpgradeType
{
    [field: SerializeField] public Zone Zone { get; private set; }
    [field: SerializeField] public int BuildingLevel { get; private set; }
    [field: SerializeField] public float GrowthTime { get; private set; }
    [field: SerializeField] public ResourceObjectData ResourceObjectData { get; private set; }
    [field: SerializeField] public float Probability { get; private set; }
}