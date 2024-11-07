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
                Debug.Log("����");
                break;
            case ELevel.Normal:
                Debug.Log("����");
                break;
            case ELevel.Hard:
                Debug.Log("�����");
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