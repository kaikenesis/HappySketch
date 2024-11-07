using TMPro;
using UnityEngine;

public class LevelButton : BaseButton
{
    public ELevel level;
    private TextMeshProUGUI text;

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

    protected override void SetInfo()
    {
        base.SetInfo();
    }

    public override void OnClicked()
    {
        base.OnClicked();

        SetLevelInfo();
        UIManager.Instance.SetExplainSubName();
    }

    private void SetLevelInfo()
    {
        UIManager.Instance.curLevel = level;
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