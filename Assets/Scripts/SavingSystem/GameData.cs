using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public string Name;
    public List<SpawnData> SpawnData;
    public BridgeData BridgeData;
    public List<BuildingData> BuildingData;
    public InventoryData InventoryData;
    public List<UpgradeData> UpgradeData;

    public GameData(string name)
    {
        Name = name;
        SpawnData = new ();
        BridgeData = new BridgeData();
        BuildingData = new ();
        InventoryData = new InventoryData();
        UpgradeData = new ();
    }

    public override string ToString()
    {
        return $"{Name}, {SpawnData.Count}, {BridgeData.ID}, {BuildingData.Count}, {InventoryData.ID}, {UpgradeData.Count}";
    }
}

[Serializable]
public class SpawnData : ISaveable
{
    public string ID { get => ObjectID; set => ObjectID = value; }
    public string ObjectID;
    public float GrowTime;
    public ResourceSpawnType[] Types;

    public void SetProbability(string name, float probability) 
    {
        foreach (var type in Types) 
        {
            if (type.Name == name)
            {
                type.Probability = probability;
            }
        }
    }
}
