using TMPro;
using UnityEngine;

public class ExplainFeverText : MonoBehaviour
{
    private TextMeshProUGUI thisText;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        thisText = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string str)
    {
        thisText.text = str;
    }
}
