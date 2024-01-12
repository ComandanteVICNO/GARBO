using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClip;

    void Start()
    {
    }

    void Update()
    {
        // Check if the 'T' key is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Check if the audio clip is not null before playing
            if (soundClip != null)
            {
                // Play the audio clip when the 'T' key is pressed
                audioSource.PlayOneShot(soundClip);
            }
            else
            {
                Debug.LogError("Audio clip is null. Make sure it is loaded correctly.");
            }
        }
    }
}
