using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LevelButton : BaseButton
{
    public ELevel level;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        SetInfo();
    }

    protected override void Init()
    {
        base.Init();
        
    }

    protected override void SetInfo()
    {
        base.SetInfo();
    }

    public override void OnClicked()
    {
        base.OnClicked();

        SetLevelInfo();
        Debug.Log("Clicked LevelButton");
        UIManager.Instance.ChangeUI(buttonType);
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    private void SetLevelInfo()
    {
        UIManager.Instance.curLevel = level;
        switch (level)
        {
            case ELevel.Easy:
                Debug.Log("쉬움");
                break;
            case ELevel.Normal:
                Debug.Log("보통");
                break;
            case ELevel.Hard:
                Debug.Log("어려움");
                break;
        }
    }
}

public enum ELevel
{
    Easy,
    Normal,
    Hard,
    MAX
}