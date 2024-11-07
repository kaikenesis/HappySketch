using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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

    public ELevel curLevel;
}
