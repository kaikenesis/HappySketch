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
                subTitleText.text = "���� ���̵�";
                break;
            case ELevel.Normal:
                subTitleText.text = "���� ���̵�";
                break;
            case ELevel.Hard:
                subTitleText.text = "����� ���̵�";
                break;
        }
    }
}
