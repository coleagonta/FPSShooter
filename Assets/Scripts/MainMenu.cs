using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;
    public void PlayGame()
    {
        gameManager.ChangeGameState(GameManager.GameState.Game);
    }

    public void ExitGame()
    {
        Debug.Log("Game Over");
        Application.Quit();
    }
}