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
        timeText.text = UIManager.Instance.maxTime.ToString() + "��";
        progressImg = GetComponent<Image>();
    }

    public void SetText()
    {
        if (UIManager.Instance.bPlayGame == false) return;
        if (UIManager.Instance.curTime < 0) return;

        timeText.text = UIManager.Instance.curTime.ToString() + "��";
    }

    public void SetProgressColor()
    {
        // �ǹ�Ÿ�Ӷ� ������������ ����
    }
}
