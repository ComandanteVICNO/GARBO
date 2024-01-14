using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public float correctVibrationTime;
    public float wrongVibrationTime;
    public float timeBetweenVibration;

    public bool isCoroutineRunning;

    private void Start()
    {
        isCoroutineRunning = false;
    }
    public void Vibrate(float durationInSeconds)
    {
        
        if (SystemInfo.supportsVibration)
        {
            
            long durationMillis = (long)(durationInSeconds * 1000f);

           
            //Handheld.Vibrate();
        }
        else
        {
            Debug.LogWarning("Vibration is not supported on this device.");
        }
    }

    public void WrongVibrate()
    {
        
        if (isCoroutineRunning) return;
        else
        {
        StartCoroutine(VibrateTwoTimes(timeBetweenVibration));

        }
    }

    IEnumerator VibrateTwoTimes(float timeToWait)
    {
        isCoroutineRunning = true;
        Vibrate(wrongVibrationTime);
        
        yield return new WaitForSecondsRealtime(timeToWait);
        Vibrate(wrongVibrationTime);
        isCoroutineRunning = false;
        
    }

}
