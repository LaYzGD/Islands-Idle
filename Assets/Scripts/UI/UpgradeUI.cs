using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private RecipeIngredientUI[] _recipeIngredientsUI;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _upgradeDescription;
    [SerializeField] private Button _craftButton;

    public void Set(CraftingRecipe recipe, int[] currentAmount)
    {
        for (int i = 0; i < _recipeIngredientsUI.Length; i++)
        {
            _recipeIngredientsUI[i].gameObject.SetActive(false);
        }

        _craftButton.interactable = false;
        _itemImage.sprite = recipe.Sprite;
        _upgradeDescription.text = recipe.Description;

        int validator = 0;

        for (int i = 0; i < recipe.Ingredients.Length; i++)
        {
            _recipeIngredientsUI[i].gameObject.SetActive(true);
            var maxAmount = recipe.Ingredients[i].Amount;
            _recipeIngredientsUI[i].Set(recipe.Ingredients[i].ResourceType.Sprite, maxAmount, currentAmount[i]);

            if (currentAmount[i] >= maxAmount)
            {
                validator++;
            }
        }

        if (validator >= recipe.Ingredients.Length)
        {
            SetCraft();
        }
    }

    private void SetCraft()
    {
        _craftButton.interactable = true;
    }
}
