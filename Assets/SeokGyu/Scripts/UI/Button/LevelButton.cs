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
                subTitleText.text = "����";
                ColorUtility.TryParseHtmlString("#FFE776FF", out color);
                thisImg.color = color;
                break;
            case ELevel.Normal:
                subTitleText.text = "����";
                ColorUtility.TryParseHtmlString("#6AA6F7FF", out color);
                thisImg.color = color;
                break;
            case ELevel.Hard:
                subTitleText.text = "�����";
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