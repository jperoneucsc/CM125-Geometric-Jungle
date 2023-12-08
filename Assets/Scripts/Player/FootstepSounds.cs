using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    [SerializeField] TempMovement playerRef;
    AudioSource audio;

    [SerializeField] AudioClip[] stepSounds;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // The update function still runs when timescale is 0, so this line fixes weird pausing issues
        if (Time.timeScale == 0)return;

        if (playerRef.isTouchGround && playerRef.rb.velocity.z > 0.0f && !audio.isPlaying)
        {
            PlayFootstepSound();
        }
    }

    void PlayFootstepSound()
    {
        audio.clip = stepSounds[Random.Range(0, stepSounds.Length)];
        audio.volume = Random.Range(0.8f, 1.0f);
        audio.pitch = Random.Range(0.95f, 1.05f);

        audio.Play();
    }
}
