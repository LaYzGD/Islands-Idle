using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataService : IDataService
{
    private ISerializer _serializer;
    private string _dataPath;
    private string _fileExtension;

    public FileDataService(ISerializer serializer)
    {
        _serializer = serializer;
        _dataPath = Application.persistentDataPath;
        _fileExtension = "json";
    }

    public string GetFullPath(string fileName) 
    {
        return Path.Combine(_dataPath, string.Concat(fileName, ".", _fileExtension));
    }

    public void Save(GameData data, bool overwrite = true)
    {
        string fileLocation = GetFullPath(data.Name);

        if (!overwrite && File.Exists(fileLocation))
        {
            throw new IOException($"The file '{data.Name}.{_fileExtension}' already exists and cannot be overwritten.");
        }

        File.WriteAllText(fileLocation, _serializer.Serialize(data));
    }

    public bool CanLoadGame(string name)
    {
        string fileLocation = GetFullPath(name);

        if (!File.Exists(fileLocation))
        {
            return false;
        }

        return true;
    }

    public GameData Load(string name)
    {
        string fileLocation = GetFullPath(name);

        if (!File.Exists(fileLocation))
        {
            throw new IOException($"No persisted GameData with name '{name}'");
        }

        return _serializer.Deserialize<GameData>(File.ReadAllText(fileLocation));
    }

    public void Delete(string name)
    {
        string fileLocation = GetFullPath(name);
        
        if (File.Exists(fileLocation))
        {
            File.Delete(fileLocation);
        }
    }

    public IEnumerable<string> ListSaves()
    {
        foreach(string file in Directory.EnumerateFiles(_dataPath)) 
        {
            if (Path.GetExtension(file) == _fileExtension)
            {
                yield return Path.GetFileNameWithoutExtension(file);
            }
        }
    }
}
