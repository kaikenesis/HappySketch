using UnityEngine;
using UnityEngine.UI;

public class InGameScene : BaseScene
{
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private Vector2 scorePosition;
    [SerializeField] private GameObject feverTextPrefab;
    [SerializeField] private Vector2 feverTextPosition;
    [SerializeField] private GameObject countDownPrefab;
    private CountDown countDown;
    [SerializeField] private GameObject timerPrefab;
    private TimeProgress timer;
    private InGameScore[] scoreTexts;
    private GameObject[] feverTexts;
    private Canvas[] feverCanvases;
    [SerializeField] private string[] feverTextStrings;
    [SerializeField] private float delayTime = 1.0f;
    private float curFrame = 1.0f;
    [SerializeField] private GameObject linePrefab;


    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        int playerNum = UIManager.Instance.playerNum;

        scoreTexts = new InGameScore[playerNum];
        feverTexts = new GameObject[playerNum];
        feverCanvases = new Canvas[playerNum];

        // Score
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

        // FeverText
        for (int i = 0; i < playerNum; i++)
        {
            float distance = 1000f * i;

            GameObject gameObject = Instantiate(feverTextPrefab);
            gameObject.transform.SetParent(transform);
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.transform.localPosition = new Vector3(feverTextPosition.x + distance, feverTextPosition.y, 0);

            ExplainFeverText explainFeverText = gameObject.GetComponentInChildren<ExplainFeverText>();
            explainFeverText.SetText(feverTextStrings[i]);

            feverCanvases[i] = gameObject.GetComponent<Canvas>();
            feverCanvases[i].enabled = false;

            feverTexts[i] = gameObject;
            
        }

        // CountDown
        {
            countDown = countDownPrefab.GetComponent<CountDown>();
        }

        // Timer
        {
            timer = timerPrefab.GetComponent<TimeProgress>();
        }

        // Line
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
            curFrame += Time.deltaTime;
            if (curFrame >= delayTime)
            {
                float time = NoteManager.Instance.noteTimeInfo.PlayTime - NoteManager.Instance.curTime;
                if (time <= 0)
                {
                    DecreaseTime();
                    StopGame();
                    // InGame 진행멈추고 결과화면 띄우기
                }
                else
                {
                    DecreaseTime();
                }
                curFrame = 0f;
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
        timer.ResetText();
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
    }

    public void DecreaseTime()
    {
        timer.SetText();
    }

    public void SetScore(int playerNum, int score)
    {
        scoreTexts[playerNum].SetText(score);
    }

    public void ActivateFeverTime()
    {
        for(int i =0;i< feverTexts.Length;i++)
        {
            feverCanvases[i].enabled = true;
        }
    }

    public void DeactivateFeverTime()
    {
        for (int i = 0; i < feverTexts.Length; i++)
        {
            feverCanvases[i].enabled = false;
        }
    }
}
