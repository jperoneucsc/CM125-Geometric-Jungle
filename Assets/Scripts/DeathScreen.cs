using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    // Pause menu implementation from:
    // https://www.youtube.com/watch?v=MNUYe0PWNNs&ab_channel=RehopeGames

    [SerializeField] GameObject deathScreen;

    [SerializeField] SceneLoader sceneLoader;

    public void ShowScreen()
    {
        Time.timeScale = 0;
        deathScreen.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        sceneLoader.LoadScene("SampleScene");
    }

    public void TitleScreen()
    {
        Time.timeScale = 1;
        sceneLoader.LoadScene("TitleScreen");
    }
}
