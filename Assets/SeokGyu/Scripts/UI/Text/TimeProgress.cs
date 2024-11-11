using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeProgress : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private float limitTime;
    private TextMeshProUGUI timeText;
    

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        timeText = textObject.GetComponent<TextMeshProUGUI>();
        timeText.text = limitTime.ToString() + "√ ";
    }

    public void Decrease()
    {
        if (limitTime <= 0) return;

        limitTime--;
        timeText.text = limitTime.ToString() + "√ ";
    }

}
