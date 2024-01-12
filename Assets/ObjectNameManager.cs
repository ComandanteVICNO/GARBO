using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
public class ObjectNameManager : MonoBehaviour
{
    public static ObjectNameManager instance;
    public GameObject[] objectUIElements;
    public TMP_Text objectName;

    public Locale pt;
    public Locale en;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        HideUI();
    }
    void Update()
    {
        CheckDragDrop();

    }

    public void SetText(string ptName, string enName)
    {
        ShowUI();
        if (LocalizationSettings.SelectedLocale == pt)
        {
            objectName.text = ptName;
        }
        else if(LocalizationSettings.SelectedLocale == en)
        {
            objectName.text = enName;
        }
    }

    public void CheckDragDrop()
    {
        if(DragAndDrop.instance == null)
        {
            HideUI();
        }

        if (!DragAndDrop.instance.isDragging)
        {
            HideUI();
        }
    }
    public void HideUI()
    {
        foreach(GameObject uiObject in objectUIElements) 
        {
            uiObject.SetActive(false);
        }
    }

    public void ShowUI() 
    {
        foreach (GameObject uiObject in objectUIElements)
        {
            uiObject.SetActive(true);
        }
    }

}
