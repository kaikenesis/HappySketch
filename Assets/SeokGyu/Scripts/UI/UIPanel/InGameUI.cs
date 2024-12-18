using UnityEngine;
using UnityEngine.UI;

public class InGameUI : BaseUI
{
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private Vector2 scorePosition;
    [SerializeField] private GameObject feverTextPrefab;
    [SerializeField] private Vector2 feverTextPosition;
    [SerializeField] private CountDown countDown;
    [SerializeField] private TimeProgress timer;
    private InGameScore[] scoreTexts;
    private GameObject[] feverTexts;
    [SerializeField] private string[] feverTextStrings;
    [SerializeField] private GameObject linePrefab;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        SetScore();
        SetFeverText();
        SetLine();
    }

    private void SetScore()
    {
        int playerNum = UIManager.Instance.playerNum;
        scoreTexts = new InGameScore[playerNum];

        for (int i = 0; i < playerNum; i++)
        {
            float distance = 1600f * i;
            GameObject gameObject = Instantiate(scorePrefab);
            gameObject.transform.SetParent(transform);
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.transform.localPosition = scorePosition;
            rect.transform.localPosition = new Vector3(scorePosition.x + distance, scorePosition.y, 0);

            scoreTexts[i] = gameObject.GetComponent<InGameScore>();
        }
    }

    private void SetFeverText()
    {
        int playerNum = UIManager.Instance.playerNum;
        feverTexts = new GameObject[playerNum];

        for (int i = 0; i < playerNum; i++)
        {
            float distance = 1000f * i;

            GameObject gameObject = Instantiate(feverTextPrefab);
            gameObject.transform.SetParent(transform);
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.transform.localPosition = new Vector3(feverTextPosition.x + distance, feverTextPosition.y, 0);

            ExplainFeverText explainFeverText = gameObject.GetComponentInChildren<ExplainFeverText>();
            explainFeverText.SetText(feverTextStrings[i]);

            feverTexts[i] = gameObject;
            feverTexts[i].gameObject.SetActive(false);
        }
    }

    private void SetLine()
    {
        int playerNum = UIManager.Instance.playerNum;
        int dist = Screen.width / playerNum;
        int posX = 0;

        for (int i = 0; i < playerNum - 1; i++)
        {
            GameObject gameObject = Instantiate(linePrefab, transform);

            gameObject.transform.SetAsFirstSibling();
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0.5f);
            rect.anchorMax = new Vector2(0, 0.5f);
            posX += dist;
            rect.transform.localPosition = new Vector3(0, 0, 0);
            rect.sizeDelta = new Vector2(6, Screen.height);

            Image img = gameObject.GetComponent<Image>();
            img.color = new Color(0, 0, 0, 1);
        }
    }

    private void Update()
    {
        UpdateGame();
    }

    private void UpdateGame()
    {
        if (UIManager.Instance.bPlayGame == true)
        {
            DecreaseTime();
            if (NoteManager.Instance.noteTimeInfo.PlayTime <= NoteManager.Instance.CurTime)
            {
                StopGame();
            }
        }
    }

    private void StopGame()
    {
        UIManager.Instance.bPlayGame = false;
        UIManager.Instance.uiDirector.FinishGame();
    }

    public override void Activate()
    {
        base.Activate();

        countDown.Activate();
        timer.ResetProgress();
    }

    public void ResetGame()
    {
        int playerNum = UIManager.Instance.playerNum;
        for (int i = 0; i < playerNum; i++)
        {
            UIManager.Instance.scores[i] = 0;
            scoreTexts[i].SetText(UIManager.Instance.scores[i]);
        }

        DeactivateFeverTime();
        timer.SetProgressColor(false);
    }

    public void DecreaseTime()
    {
        timer.UpdateProgress();
    }

    public void UpdateScore(int playerNum, int score)
    {
        scoreTexts[playerNum].SetText(score);
    }

    public void ActivateFeverTime()
    {
        for(int i =0;i< feverTexts.Length;i++)
        {
            feverTexts[i].gameObject.SetActive(true);
        }
        timer.SetProgressColor(true);
    }

    private void DeactivateFeverTime()
    {
        for (int i = 0; i < feverTexts.Length; i++)
        {
            feverTexts[i].gameObject.SetActive(false);
        }
    }
}
