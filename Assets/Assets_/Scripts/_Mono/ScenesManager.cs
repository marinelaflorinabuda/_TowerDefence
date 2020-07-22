using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private const string mainSceneName = "TowerDefenceScene";
    private const string gameOverSceneName = "GameOverScene";
    public void PlayGame()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    public void GameIsOver()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Game exit");
        Application.Quit();
    }

}
