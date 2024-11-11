using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIDirector : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selectLevel;
    [SerializeField] private GameObject explain;
    [SerializeField] private GameObject inGame;

    private bool isDebug = false;

    public ELevel curLevel;

    private void Init()
    {
        mainMenu.GetComponent<Canvas>().enabled = true;
        selectLevel.GetComponent<Canvas>().enabled = false;
        explain.GetComponent<Canvas>().enabled = false;
        inGame.GetComponent<Canvas>().enabled = false;
    }

    public void ChangeUI(EUIType curUIType, EButtonType buttonType)
    {
        switch (curUIType)
        {
            case EUIType.MainMenu:
                mainMenu.GetComponent<Canvas>().enabled = false;
                break;
            case EUIType.SelectLevel:
                selectLevel.GetComponent<Canvas>().enabled = false;
                break;
            case EUIType.Explain:
                explain.GetComponent<Canvas>().enabled = false;
                break;
            case EUIType.InGame:
                inGame.GetComponent<Canvas>().enabled = false;
                break;
        }

        switch (buttonType)
        {
            case EButtonType.Start:
                selectLevel.GetComponent<Canvas>().enabled = true;
                break;
            case EButtonType.SelectLevel:
                explain.GetComponent<Canvas>().enabled = true;
                explain.GetComponent<ExplainScene>().SetText(curLevel);
                break;
            case EButtonType.GameStart:
                inGame.GetComponent<InGameScene>().Activate();
                break;
            case EButtonType.Retry:
                break;
            case EButtonType.MainMenu:
                mainMenu.GetComponent<Canvas>().enabled = true;
                break;
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "디버깅"))
        {
            isDebug = !isDebug;
        }

        if(isDebug == true)
        {
            if(GUI.Button(new Rect(0, 50, 100, 50), "시간 감소"))
            {
                inGame.GetComponent<InGameScene>().DecreaseTime();
            }
            if (GUI.Button(new Rect(0, 100, 100, 50), "점수 증가"))
            {
                inGame.GetComponent<InGameScene>().InCreaseScore(10, 30);
            }
            if (GUI.Button(new Rect(0, 150, 100, 50), "피버 타임"))
            {
                inGame.GetComponent<InGameScene>().ActivateFeverTime();
            }
        }
    }
}
