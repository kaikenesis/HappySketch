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
        progressImg = GetComponent<Image>();
    }

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        timeText.text = NoteManager.Instance.noteTimeInfo.PlayTime.ToString("F0") + "초";
    }

    public void ResetText()
    {
        timeText.text = NoteManager.Instance.noteTimeInfo.PlayTime.ToString("F0") + "초";
    }

    public void SetText()
    {
        float time = NoteManager.Instance.noteTimeInfo.PlayTime - NoteManager.instance.curTime;
        if (UIManager.Instance.bPlayGame == false) return;
        if (time < 0) return;

        timeText.text = time.ToString("F0") + "초";
    }

    public void SetFeverColor()
    {
        // 피버타임때 무지개색으로 변경
    }

    public void SetDefaultColor()
    {

    }
}
