using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string loadingScreenPath = "LoadingScreen";

    public void LoadScene(string scenePath)
    {
        LoadScenePath.loadScenePath = scenePath;
        SceneManager.LoadScene(loadingScreenPath);
    }
}
