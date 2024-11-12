using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private GameObject scoreTextObject;
    private TextMeshProUGUI scoreText;
    private int defaultScore = 0;

    private void Awake()
    {
        scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        scoreText.text = defaultScore + "M";
    }

    public void SetText(int score)
    {
        scoreText.text = score + "M";
    }
}
