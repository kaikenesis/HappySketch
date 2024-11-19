using TMPro;
using UnityEngine;

public class InGameScore : MonoBehaviour
{
    [SerializeField] private GameObject scoreTextObject;
    private TextMeshProUGUI thisText;
    private int defaultScore = 0;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        thisText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        thisText.text = defaultScore + "M";
    }

    public void SetText(int score)
    {
        thisText.text = score + "M";
    }
}
