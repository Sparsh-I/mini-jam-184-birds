using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string levelToLoad;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
