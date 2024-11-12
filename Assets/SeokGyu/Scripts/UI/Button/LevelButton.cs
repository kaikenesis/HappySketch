using TMPro;

public class LevelButton : ChangeUIButton
{
    public ELevel level;
    private TextMeshProUGUI subTitleText;
    

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        subTitleText = GetComponentInChildren<TextMeshProUGUI>();

        switch (level)
        {
            case ELevel.Easy:
                subTitleText.text = "쉬움";
                break;
            case ELevel.Normal:
                subTitleText.text = "보통";
                break;
            case ELevel.Hard:
                subTitleText.text = "어려움";
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
                subTitleText.text = "쉬움";
                break;
            case ELevel.Normal:
                subTitleText.text = "보통";
                break;
            case ELevel.Hard:
                subTitleText.text = "어려움";
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