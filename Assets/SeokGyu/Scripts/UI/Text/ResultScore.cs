using TMPro;
using UnityEngine;

public class ResultScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI scoreText;

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
