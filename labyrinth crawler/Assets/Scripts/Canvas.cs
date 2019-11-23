using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public void LoadDeathScreen()
    {
        gameObject.SetActive(true);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Retry()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void NextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
