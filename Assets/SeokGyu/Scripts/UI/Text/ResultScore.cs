using TMPro;
using UnityEngine;

public class ResultScore : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject scoreObject;
    private TextMeshProUGUI resultText;
    private TextMeshProUGUI score;


    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        resultText = textObject.GetComponent<TextMeshProUGUI>();
        score = scoreObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetText(bool bWinner, int playerNum)
    {
        if(bWinner == true)
        {
            resultText.text = "WIN!";
            score.text = UIManager.Instance.scores[playerNum].ToString() + "Á¡";
        }
        else
        {
            resultText.text = "LOSE";
            score.text = UIManager.Instance.scores[playerNum].ToString() + "Á¡";
        }
    }
}
