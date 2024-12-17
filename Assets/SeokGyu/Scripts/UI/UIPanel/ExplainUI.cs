using TMPro;
using UnityEngine;

public class ExplainUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI subTitleText;

    public void SetText(ELevel level)
    {
        switch (level)
        {
            case ELevel.Easy:
                subTitleText.text = "쉬움 난이도";
                break;
            case ELevel.Normal:
                subTitleText.text = "보통 난이도";
                break;
            case ELevel.Hard:
                subTitleText.text = "어려움 난이도";
                break;
        }
    }
}
