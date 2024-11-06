using System;
using TMPro;
using UnityEditor;
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
        float xPos = 0;
        if ((int)level % 2 == 0) xPos = -90 * ((int)level / 2);
        else if ((int)level % 2 == 1) xPos = -45 - 90 * ((int)level / 2);

        starImg.transform.localPosition = new Vector3(xPos, starImg.transform.localPosition.y, 0);
        for (int i = 0; i < (int)level; i++)
        {
            xPos += 90;
            GameObject ob = Instantiate(starImg);
            ob.transform.SetParent(transform);
            ob.transform.localPosition = new Vector3(xPos, starImg.transform.localPosition.y, 0);
        }
    }
}

enum ELevel
{
    Easy,
    Normal,
    Hard
}