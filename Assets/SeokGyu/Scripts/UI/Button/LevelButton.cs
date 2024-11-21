using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : ChangeUIButton
{
    public ELevel level;
    private TextMeshProUGUI subTitleText;
    private Image thisImg;

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        subTitleText = GetComponentInChildren<TextMeshProUGUI>();
        thisImg = GetComponent<Image>();
        Color color;

        switch (level)
        {
            case ELevel.Easy:
                subTitleText.text = "쉬움";
                ColorUtility.TryParseHtmlString("#FFE776FF", out color);
                thisImg.color = color;
                break;
            case ELevel.Normal:
                subTitleText.text = "보통";
                ColorUtility.TryParseHtmlString("#6AA6F7FF", out color);
                thisImg.color = color;
                break;
            case ELevel.Hard:
                subTitleText.text = "어려움";
                ColorUtility.TryParseHtmlString("#F75D76FF", out color);
                thisImg.color = color;
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
                NoteManager.instance.SetLevel(0);
                break;
            case ELevel.Normal:
                subTitleText.text = "보통";
                NoteManager.instance.SetLevel(1);
                break;
            case ELevel.Hard:
                subTitleText.text = "어려움";
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