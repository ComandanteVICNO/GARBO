using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SocialPlatforms;

public class LocalizationMananger : MonoBehaviour
{
    private bool isActive = false;
    public GameObject PTButton;
    public GameObject ENButton;
    public Locale ptLocale;
    public Locale enLocale;
    string LanguagePrefs = "LanguageID";

    private void Start()
    {
        if(PlayerPrefs.HasKey(LanguagePrefs))
        {
            int playerPrefID = PlayerPrefs.GetInt(LanguagePrefs);
            Debug.Log(playerPrefID);
            ChangeLanguage(playerPrefID);
        }
    }

    private void Update()
    {
        
        if(LocalizationSettings.SelectedLocale == ptLocale)
        {
            PTButton.SetActive(true);
            ENButton.SetActive(false);

        }
        else if(LocalizationSettings.SelectedLocale == enLocale)
        {
            PTButton.SetActive(false);
            ENButton.SetActive(true);
        }
        
    }

    public void ChangeLanguage(int localID)
    {
        if (isActive) return;
        StartCoroutine(SetLanguage(localID));
    }


    IEnumerator SetLanguage(int localID)
    {
        isActive = true;
        yield return LocalizationSettings.InitializationOperation;
        if(localID == 0)
        {
            LocalizationSettings.SelectedLocale = enLocale;
        }
        else if(localID == 1)
        {
            LocalizationSettings.SelectedLocale = ptLocale;
        }
        
        PlayerPrefs.SetInt(LanguagePrefs, localID);
        PlayerPrefs.Save();
        isActive = false;
    }





}
