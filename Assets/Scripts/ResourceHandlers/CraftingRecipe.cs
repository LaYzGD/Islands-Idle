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
    public Zone Zone;
    public int BuildingLevel;
    public float GrowthTime;
    public ResourceObjectData ResourceObjectData;
    public float Probability;
}