using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplainScene : BaseScene
{
    [SerializeField] private GameObject TextObject;
    private TextMeshProUGUI subTitleText;

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        subTitleText = TextObject.GetComponent<TextMeshProUGUI>();
    }

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
