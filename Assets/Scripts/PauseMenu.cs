using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Pause menu implementation from:
    // https://www.youtube.com/watch?v=MNUYe0PWNNs&ab_channel=RehopeGames

    [SerializeField] GameObject pauseMenu;
    public bool isActive = false;

    [SerializeField] SceneLoader sceneLoader;

    public void Pause()
    {
        Time.timeScale = 0;
        isActive = true;
        pauseMenu.SetActive(isActive);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isActive = false;
        pauseMenu.SetActive(isActive);
    }

    public void TitleScreen()
    {
        Time.timeScale = 1;
        sceneLoader.LoadScene("TitleScreen");
    }
}
