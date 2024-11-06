using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LevelButton : BaseButton
{
    [SerializeField]
    public ELevel level { get; private set; }

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
        SetLevelInfo();
    }

    private void SetLevelInfo()
    {
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
    Hard
}