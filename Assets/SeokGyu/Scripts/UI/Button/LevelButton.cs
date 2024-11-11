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
                text.text = "����";
                break;
            case ELevel.Normal:
                text.text = "����";
                break;
            case ELevel.Hard:
                text.text = "�����";
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
                text.text = "����";
                break;
            case ELevel.Normal:
                text.text = "����";
                break;
            case ELevel.Hard:
                text.text = "�����";
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