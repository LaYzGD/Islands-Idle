using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuView : MonoBehaviour
{
    [SerializeField] private string _gameSceneName = "GameScene";
    [SerializeField] private string _gameName = "Game";
    private SaveAndLoad _saveAndLoad;

    private void Start()
    {
        _saveAndLoad = SaveAndLoad.Instance;
    }

    public void StartNewGame()
    {
        _saveAndLoad.NewGame();
        SceneManager.LoadScene(_gameSceneName);
    }

    public void LoadGame()
    {
        _saveAndLoad.LoadGame(_gameName);
        SceneManager.LoadScene(_gameSceneName);
    }
}
