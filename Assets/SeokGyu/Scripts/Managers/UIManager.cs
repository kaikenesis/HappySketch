using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ELevel curLevel;

    private static UIManager instance;
    private GameObject mainMenuUI;
    private GameObject selectLevelUI;
    private GameObject explainUI;
    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject newGameObject = new GameObject("UIManager");
                instance = newGameObject.AddComponent<UIManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
        
        Init();
    }

    void Init()
    {
        mainMenuUI = GameObject.Find("MainMenu");
        selectLevelUI = GameObject.Find("SelectLevel");
        explainUI = GameObject.Find("Explain");

        mainMenuUI.GetComponent<Canvas>().enabled = true;
        selectLevelUI.GetComponent<Canvas>().enabled = false;
        explainUI.GetComponent<Canvas>().enabled = false;

        
    }

    public void ChangeUI(EUIType curUIType, EButtonType buttonType)
    {
        switch (curUIType)
        {
            case EUIType.MainMenu:
                mainMenuUI.GetComponent<Canvas>().enabled = false;
                break;
            case EUIType.SelectLevel:
                selectLevelUI.GetComponent<Canvas>().enabled = false;
                break;
            case EUIType.Explain:
                explainUI.GetComponent<Canvas>().enabled = false;
                break;
            case EUIType.InGame:
                break;
        }

        switch (buttonType)
        {
            case EButtonType.Start:
                selectLevelUI.GetComponent<Canvas>().enabled = true;
                break;
            case EButtonType.SelectLevel:
                explainUI.GetComponent<Canvas>().enabled = true;
                break;
            case EButtonType.GameStart:
                break;
            case EButtonType.Retry:
                break;
            case EButtonType.MainMenu:
                mainMenuUI.GetComponent<Canvas>().enabled = true;
                break;
        }
    }

    public void SetExplainSubName()
    {
        Transform child = explainUI.transform.Find("SubNameText");
        Debug.Log(child);
        TextMeshProUGUI subNameText = child.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(subNameText);

        switch (curLevel)
        {
            case ELevel.Easy:
                subNameText.text = "쉬움 난이도";
                break;
            case ELevel.Normal:
                subNameText.text = "보통 난이도";
                break;
            case ELevel.Hard:
                subNameText.text = "어려움 난이도";
                break;
        }


    }
}
