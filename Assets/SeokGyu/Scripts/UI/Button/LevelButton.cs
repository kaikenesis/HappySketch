using System;
using TMPro;
using UnityEngine;

public class LevelButton : BaseButton
{
    [SerializeField]
    private ELevel level;
    private TextMeshProUGUI textName;
    [SerializeField]
    private GameObject starImg;

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        SetInfo();
    }

    protected override void Init()
    {
        base.Init();

        textName = GetComponentInChildren<TextMeshProUGUI>();

        SetLevelName();
        SetStarCount();
    }

    protected override void SetInfo()
    {
        base.SetInfo();
    }

    public override void OnClicked()
    {
        // 키설명 화면으로 전환
    }

    private void SetLevelName()
    {
        switch (level)
        {
            case ELevel.Easy:
                textName.SetText("Easy");
                break;
            case ELevel.Normal:
                textName.SetText("Normal");
                break;
            case ELevel.Hard:
                textName.SetText("Hard");
                break;
        }
    }

    private void SetStarCount()
    {
        for (int i = 0; i < (int)level; i++)
        {
            GameObject ob = Instantiate(starImg);
            ob.transform.SetParent(transform);
            ob.transform.position = starImg.transform.position;
        }
    }
}

enum ELevel
{
    Easy,
    Normal,
    Hard
}