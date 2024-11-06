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
    Hard
}