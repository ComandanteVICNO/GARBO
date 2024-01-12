using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinSound : MonoBehaviour
{
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioSource audioSource;

    DragAndDrop dragAndDrop;

    int numbeOfOctaves = 3;
    private void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        dragAndDrop = other.GetComponent<DragAndDrop>();
        PlaySound(openSound);
    }

    private void OnTriggerStay(Collider other)
    {
        if (dragAndDrop.isDragging) return;
        else
        {
            PlaySound(closeSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlaySound(closeSound);
    }



    private void SetRandomPitch()
    {
        float pitch = 1.0f;

        for(int i = 0; i < numbeOfOctaves; i++)
        {
            pitch *= 1.059463f;
        }

        audioSource.pitch = pitch;

    }

    private void PlaySound(AudioClip clip)
    {
        // Set the clip for the AudioSource
        audioSource.clip = clip;

        if(clip = openSound)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        }

        // Play the sound with the updated pitch
        audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
    }

}
