using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LimitFramerate : MonoBehaviour
{
    private int refreshRate = 120;

    public float updateInterval = 0.5f; // How often to update the FPS value.
    private float lastInterval;
    private int frames = 0;
    private float fps;
    
    private void Start()
    {
        Application.targetFrameRate = refreshRate;
        QualitySettings.vSyncCount = 0;
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;

        if (timeNow > lastInterval + updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
        }

    }
}