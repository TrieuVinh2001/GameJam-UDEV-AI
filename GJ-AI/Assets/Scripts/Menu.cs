using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public string levelName;

    public void LoadLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }

    public void Replay()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
