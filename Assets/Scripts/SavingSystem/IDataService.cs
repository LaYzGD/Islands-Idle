using System.Collections.Generic;

public interface IDataService
{
    public void Save(GameData data, bool overwrite = true);
    public GameData Load(string name);
    public bool CanLoadGame(string name);
    public void Delete(string name);
    public IEnumerable<string> ListSaves();
}
