using UnityEngine;
using UnityEngine.UI;

public class ResultUI : BaseUI
{
    [SerializeField] private GameObject resultScoreObject;
    [SerializeField] private GameObject backImgObject;
    [SerializeField] private float distance = 500;
    private ResultScore[] resultScores;
    private Image[] backImgs;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        int playerNum = UIManager.Instance.playerNum;
        resultScores = new ResultScore[playerNum];
        backImgs = new Image[playerNum];
        float backImgSizeX = Screen.width / playerNum;
        float backImgPosX = backImgSizeX * -0.5f;

        for (int i = 0; i < playerNum; i++)
        {
            distance *= -1;
            GameObject resultScore = Instantiate(resultScoreObject);
            resultScore.transform.SetParent(transform);
            resultScore.transform.localPosition = new Vector3(distance, 0, 0);
            resultScore.transform.SetAsFirstSibling();
            resultScores[i] = resultScore.GetComponent<ResultScore>();
        }

        for (int i = 0; i < playerNum; i++)
        {
            GameObject backImg = Instantiate(backImgObject);
            backImg.transform.SetParent(transform);
            RectTransform backImgRect = backImg.GetComponent<RectTransform>();
            backImgRect.sizeDelta = new Vector2(backImgSizeX, Screen.height);
            backImg.transform.localPosition = new Vector3(backImgPosX, 0, 0);
            backImg.transform.SetAsFirstSibling();
            backImgPosX += backImgSizeX;
            backImgs[i] = backImg.GetComponent<Image>();
        }
    }

    public void SetWinner(int playerNum)
    {
        for (int i = 0; i < UIManager.Instance.playerNum; i++)
        {
            if(i != playerNum)
            {
                resultScores[i].SetText(false, i);
                backImgs[i].color = new Color(0, 0, 0, 0.25f);
            }
            else
            {
                resultScores[i].SetText(true, i);
                backImgs[i].color = new Color(1, 1, 0, 0.25f);
            }
        }
    }
}
