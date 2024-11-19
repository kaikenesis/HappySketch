using TMPro;
using UnityEngine;

public class ResultScore : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject scoreObject;
    private TextMeshProUGUI resultText;
    private TextMeshProUGUI scoreText;


    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        resultText = textObject.GetComponent<TextMeshProUGUI>();
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetText(bool bWinner, int playerNum)
    {
        if(bWinner == true)
        {
            resultText.text = "WIN!";
            scoreText.text = UIManager.Instance.scores[playerNum].ToString() + "Á¡";
        }
        else
        {
            resultText.text = "LOSE";
            scoreText.text = UIManager.Instance.scores[playerNum].ToString() + "Á¡";
        }
    }
}
