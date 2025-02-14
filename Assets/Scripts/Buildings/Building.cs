using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Harvester[] _harvesters;

    private VFXPool _pool;

    public void Initialize(VFXPool vfxPool, InventoryController inventoryController)
    {
        _pool = vfxPool;
        
        foreach (var harvester in _harvesters)
        {
            harvester.Initialize(_pool, inventoryController);
        }
    }
}
