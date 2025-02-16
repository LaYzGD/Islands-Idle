using System.Linq;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    [SerializeField] private ResourceType[] _allItems;
    
    public ResourceType GetItemByName(string name) 
    {
        foreach (var item in _allItems) 
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }
}
