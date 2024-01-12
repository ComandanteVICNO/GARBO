using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class RandomFactManager : MonoBehaviour
{
    public TMP_Text factsText;

    [Multiline] public string[] ptFacts;
    [Multiline] public string[] enFacts;

    public Locale ptLocale;
    public Locale enLocale;

    string currentLocale;

    private void OnEnable()
    {
        
        SetRandomFact();

    }

    void Start()
    {
        
        SetRandomFact();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CheckCurrentLocal()
    {
        if (LocalizationSettings.SelectedLocale == ptLocale)
        {
            currentLocale = "PT";
        }
        if (LocalizationSettings.SelectedLocale == enLocale)
        {
            currentLocale = "EN";
        }
    }

    public string GetRandomFact()
    {
        CheckCurrentLocal();
        if (currentLocale == "PT")
        {
            int randomIndex = UnityEngine.Random.Range(0, ptFacts.Length);
            return ptFacts[randomIndex];

        }

        else if(currentLocale == "EN")
        {
            int randomIndex = UnityEngine.Random.Range(0, enFacts.Length);
            return enFacts[randomIndex];
        }

        else
        {
            return null;
        }
        
    }

    public void SetRandomFact()
    {
        factsText.text = GetRandomFact();
    }

    public void FactsButton()
    {
        
        SetRandomFact();
    }

}
