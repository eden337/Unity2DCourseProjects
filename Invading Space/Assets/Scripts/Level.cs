using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    [SerializeField] float gameoverDelay = 2f;
    public void LoadGameOver()
    {
        StartCoroutine(GameOverCorutine());
        
    }

    IEnumerator GameOverCorutine()
    {
        yield return new WaitForSeconds(gameoverDelay);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene()
    {
        GameSession gs = FindObjectOfType<GameSession>();
        if (gs != null)
        {
            gs.ResetGame();
        }
        SceneManager.LoadScene("Game");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
