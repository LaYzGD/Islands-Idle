using UnityEngine;

public class BridgeOpenUI : MonoBehaviour
{
    [SerializeField] private TriggerZone _triggerZone;
    [SerializeField] private CraftingRecipe _recipe;
    [SerializeField] private RecipeIngredientUI[] _ingredients;
    [SerializeField] private Bridge _bridge;

    private InventoryController _inventoryController;

    public void Initialize(InventoryController inventory)
    {
        _inventoryController = inventory;
    }

    public void OnEnable()
    {
        var length = _recipe.Ingredients.Length;
        if (_recipe.Ingredients.Length > _ingredients.Length)
        {
            length = _ingredients.Length;
        }

        foreach (var ingredient in _ingredients)
        {
            ingredient.gameObject.SetActive(false);
        }

        var validator = 0;
        for (int i = 0; i < length; i++)
        {
            _ingredients[i].gameObject.SetActive(true);
            var maxAmount = _recipe.Ingredients[i].Amount;
            var currentAmount = _inventoryController.GetItemAmount(_recipe.Ingredients[i].ResourceType);
            _ingredients[i].Set(_recipe.Ingredients[i].ResourceType.Sprite, maxAmount, currentAmount);
            
            if (currentAmount >= maxAmount)
            {
                validator++;
            }
        }
        if (validator >= length)
        {
            _bridge.ShowBridge();
            
            foreach (var ingredient in _recipe.Ingredients) 
            {
                _inventoryController.TryRemoveItems(ingredient.ResourceType, ingredient.Amount);
            }
            _triggerZone.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
