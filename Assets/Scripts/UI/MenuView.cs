using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [SerializeField] private string _gameSceneName = "GameScene";
    [SerializeField] private string _gameName = "Game";
    [SerializeField] private Button _loadGameButton;
    private SaveAndLoad _saveAndLoad;

    private void Start()
    {
        _saveAndLoad = SaveAndLoad.Instance;
        if (_saveAndLoad.CanLoadGame(_gameName))
        {
            _loadGameButton.interactable = true;
        }
        else
        {
            _loadGameButton.interactable = false;
        }
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
