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