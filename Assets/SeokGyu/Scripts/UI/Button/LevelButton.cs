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
                subTitleText.text = "����";
                break;
            case ELevel.Normal:
                subTitleText.text = "����";
                break;
            case ELevel.Hard:
                subTitleText.text = "�����";
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
                subTitleText.text = "����";
                NoteManager.instance.SetLevel(0);
                break;
            case ELevel.Normal:
                subTitleText.text = "����";
                NoteManager.instance.SetLevel(1);
                break;
            case ELevel.Hard:
                subTitleText.text = "�����";
                NoteManager.instance.SetLevel(2);
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