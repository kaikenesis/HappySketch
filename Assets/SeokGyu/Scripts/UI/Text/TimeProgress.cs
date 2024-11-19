using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeProgress : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    private TextMeshProUGUI timeText;
    private Image progressImg;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        timeText = textObject.GetComponentInChildren<TextMeshProUGUI>();
        timeText.text = UIManager.Instance.maxTime.ToString() + "초";
        progressImg = GetComponent<Image>();
    }

    public void SetText()
    {
        if (UIManager.Instance.bPlayGame == false) return;
        if (UIManager.Instance.curTime < 0) return;

        timeText.text = UIManager.Instance.curTime.ToString() + "초";
    }

    public void SetProgressColor()
    {
        // 피버타임때 무지개색으로 변경
    }
}
