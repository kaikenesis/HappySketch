using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : ChangeUIButton
{
    public ELevel level;
    private TextMeshProUGUI text;
    

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        text = GetComponentInChildren<TextMeshProUGUI>();

        switch (level)
        {
            case ELevel.Easy:
                text.text = "쉬움";
                break;
            case ELevel.Normal:
                text.text = "보통";
                break;
            case ELevel.Hard:
                text.text = "어려움";
                break;
        }
    }

    public override void OnClicked()
    {
        SetLevelInfo();
        base.OnClicked();
    }

    private void SetLevelInfo()
    {
        uiDirector.curLevel = level;
        switch (level)
        {
            case ELevel.Easy:
                text.text = "쉬움";
                break;
            case ELevel.Normal:
                text.text = "보통";
                break;
            case ELevel.Hard:
                text.text = "어려움";
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