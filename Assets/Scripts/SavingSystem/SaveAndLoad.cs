using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveAndLoad : Singleton<SaveAndLoad>
{
    [field: SerializeField] public GameData GameData { get; private set; }

    private IDataService _dataService;

    protected override void Awake()
    {
        base.Awake();

        _dataService = new FileDataService(new JsonSerializer());
        
    }

    public void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new ()
        
    {
        var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
        if (entity != null) 
        {
            if (data == null)
            {
                data = new TData { ID = entity.ID };
            }
            entity.Bind(data);
        }
    }

    public void Bind<T, TData>(List<TData> datas) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
    {
        var entities = FindObjectsByType<T>(FindObjectsSortMode.None);

        foreach (var entity in entities)
        {
            var data = datas.FirstOrDefault(d => d.ID == entity.ID);
            if (data == null)
            {
                data = new TData { ID = entity.ID };
                datas.Add(data);
            }
            entity.Bind(data);
        }
    }

    public void NewGame()
    {
        GameData = new GameData("Game");
    }

    public void SaveGame()
    {
        _dataService.Save(GameData);
    }

    public void LoadGame(string gameName) 
    {
        GameData = _dataService.Load(gameName);
    }
}

public interface ISaveable 
{
    public string ID { get; set; }
}

public interface IBind<TData> where TData : ISaveable 
{
    public string ID { get; set; }
    public void Bind(TData data);
}
