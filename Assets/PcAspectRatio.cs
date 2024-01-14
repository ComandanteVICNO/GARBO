using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcAspectRatio : MonoBehaviour
{
    void Start()
    {
        if (IsDesktopPlatform())
        {
            Screen.SetResolution(720, 1080, true);
        }
    }

    bool IsDesktopPlatform()
    {
        return Application.platform == RuntimePlatform.WindowsPlayer ||
               Application.platform == RuntimePlatform.OSXPlayer ||
               Application.platform == RuntimePlatform.LinuxPlayer;
    }

}
