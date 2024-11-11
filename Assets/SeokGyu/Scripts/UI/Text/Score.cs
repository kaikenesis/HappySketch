using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private GameObject scoreTextObject;
    private TextMeshProUGUI scoreText;
    private int scoreIndex = 0;

    private void Awake()
    {
        scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        scoreText.text = scoreIndex + "M";
    }

    public void SetText(int score)
    {
        scoreIndex += score;
        scoreText.text = scoreIndex + "M";
    }
}
