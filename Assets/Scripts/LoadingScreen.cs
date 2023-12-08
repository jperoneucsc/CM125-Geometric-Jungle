using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    // Implementation from:
    // https://www.youtube.com/watch?v=YMj2qPq9CP8&ab_channel=Brackeys

    [SerializeField] Slider slider;

    void Start()
    {
        if (LoadScenePath.loadScenePath != null)
        {
            StartCoroutine(LoadAsynchronously());
        }
        else
        {
            Debug.Log("Invalid scene path");
        }
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadScenePath.loadScenePath);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
