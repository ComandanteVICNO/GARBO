using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPickupSound : MonoBehaviour
{
    public AudioClip pickupClip;
    public AudioSource audioSource;
    bool soundPlayed = false;
    

    // Update is called once per frame
    void Update()
    {
        if(DragAndDrop.instance != null)
        {
            PlayIfDragged();
        }

        if(DragAndDrop.instance == null)
        {
            soundPlayed = false;
        }
    }

    public void PlayIfDragged()
    {
        if (soundPlayed) return;
        if (DragAndDrop.instance.isDragging)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(pickupClip);

            soundPlayed = true;
        }

    }

}
