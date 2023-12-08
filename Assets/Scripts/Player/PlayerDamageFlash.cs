using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageFlash : MonoBehaviour
{
    [SerializeField] GameObject[] skins;

    bool flashEnabled = false;
    float flashTime = 0.1f;
    float visibleTime = 0.1f;

    void Start()
    {
        DisableFlash();
    }

    IEnumerator FlashTimer()
    {
        for (int i=0; i < skins.Length; i++)
        {
            skins[i].SetActive(false);
        }
        yield return new WaitForSeconds(flashTime);
        if (flashEnabled)
        {
            StartCoroutine(VisibleTimer());
        }
    }

    IEnumerator VisibleTimer()
    {
        for (int i=0; i < skins.Length; i++)
        {
            skins[i].SetActive(true);
        }
        yield return new WaitForSeconds(visibleTime);
        if (flashEnabled)
        {
            StartCoroutine(FlashTimer());
        }
    }

    public void EnableFlash()
    {
        flashEnabled = true;
        StartCoroutine(FlashTimer());
    }

    public void DisableFlash()
    {
        flashEnabled = false;
        for (int i=0; i < skins.Length; i++)
        {
            skins[i].SetActive(true);
        }
    }
}
