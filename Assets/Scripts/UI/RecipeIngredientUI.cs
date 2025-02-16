using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeIngredientUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private Outline _outline;
    [SerializeField] private Color _enoughIngredientColor = Color.green;
    [SerializeField] private Color _notEnoughIngredientColor = Color.red;

    public void Set(Sprite sprite, int maxAmount, int currentAmount)
    {
        _icon.sprite = sprite;
        _amount.text = $"{currentAmount}/{maxAmount}";

        _outline.effectColor = currentAmount >= maxAmount ? _enoughIngredientColor : _notEnoughIngredientColor;
    }
}
