using TMPro;
using UnityEngine;

public class InGameScore : MonoBehaviour
{
    [SerializeField] private GameObject scoreTextObject;
    private TextMeshProUGUI scoreText;
    private int defaultScore = 0;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        scoreText.text = defaultScore + "M";
    }

    public void SetText(int score)
    {
        scoreText.text = score + "M";
    }
}
