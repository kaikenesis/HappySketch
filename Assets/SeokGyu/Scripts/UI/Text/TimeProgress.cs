using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeProgress : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    private TextMeshProUGUI timeText;
    

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        timeText = textObject.GetComponentInChildren<TextMeshProUGUI>();
        timeText.text = UIManager.Instance.maxTime.ToString() + "√ ";
    }

    public void SetText()
    {
        if (UIManager.Instance.bPlayGame == false) return;
        if (UIManager.Instance.curTime < 0) return;

        int time = (int)UIManager.Instance.curTime;
        timeText.text = time.ToString() + "√ ";
    }

}
