using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSFX : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (sounds.Length == 0) return;

        audio.clip = sounds[Random.Range(0, sounds.Length)];
        audio.volume = Random.Range(0.8f, 1.0f);
        audio.pitch = Random.Range(0.80f, 1.10f);

        audio.Play();
    }
}
