using System;
using UnityEngine;

public class Building : MonoBehaviour, IBind<BuildingData>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Harvester[] _harvesters;
    [SerializeField] private string _ID;
    public string ID { get => _ID; set => _ID = value; }

    private BuildingData _buildingData;
    private VFXPool _pool;

    private string _setupString = "SetUp";
    private string _setdownString = "SetDown";


    public void Initialize(VFXPool vfxPool, InventoryController inventoryController)
    {
        _pool = vfxPool;

        foreach (var harvester in _harvesters)
        {
            harvester.Initialize(_pool, inventoryController);
        }
    }

    public void Enable()
    {
        _animator.SetTrigger(_setupString);
        _buildingData.IsBuilded = true;
    }

    public void Disable()
    {
        _animator.SetTrigger(_setdownString);
        _buildingData.IsBuilded = false;
    }

    void IBind<BuildingData>.Bind(BuildingData data)
    {
        _buildingData = data;

        if (data.ID == null || data.ID == string.Empty)
        {
            _buildingData.ID = ID;
            _buildingData.IsBuilded = false;
        }

        if (_buildingData.IsBuilded)
        {
            Enable();
        }
    }
}

[Serializable]
public class BuildingData : ISaveable
{
    public string ID { get => ObjectID; set => ObjectID = value; }
    public string ObjectID;
    public bool IsBuilded;
}
