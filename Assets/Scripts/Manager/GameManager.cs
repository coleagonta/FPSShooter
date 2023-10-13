using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChanged;
    private int _win;
    private int _lose;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _win = PlayerPrefs.GetInt("Win");
        _lose = PlayerPrefs.GetInt("Lose");
        ChangeGameState(GameState.Menu);
    }

    public void ChangeGameState(GameState state)
    {
        Cursor.lockState = CursorLockMode.None;
        switch (state)
        {
            case GameState.Menu:
                SwitchToMenu();
                break;

            case GameState.Game:
                SwitchToMainScene();
                break;
            case GameState.Lose:
                _lose++;
                PlayerPrefs.SetInt("Lose", _lose);
                SwitchToMenu();
                break;
            case GameState.Win:
                _win++;
                PlayerPrefs.SetInt("Win", _win);
                SwitchToMenu();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        
        OnGameStateChanged?.Invoke(state);
    }

    public enum GameState
    {
        Menu,
        Game,
        Lose,
        Win 
    }
    
    public void SwitchToMainScene()
    {
        SceneManager.LoadScene(1);
    }
    public void SwitchToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
