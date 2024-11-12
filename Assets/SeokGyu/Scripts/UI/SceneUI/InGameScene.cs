using UnityEngine;

public class InGameScene : BaseScene
{
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private Vector2 scorePosition;
    [SerializeField] private GameObject feverText;
    [SerializeField] private Vector2 feverTextPosition;
    [SerializeField] private GameObject decideNode;
    [SerializeField] private Vector2[] nodesPosition;
    [SerializeField] private float nodeWidth;
    [SerializeField] private float nodeHeight;
    [SerializeField] private GameObject countDownObject;
    private CountDown countDown;
    [SerializeField] private GameObject timerObject;
    private TimeProgress timer;
    private Score[] scoreTexts;
    private GameObject[] feverTexts;
    private Canvas[] feverCanvases;
    [SerializeField] private float delayTime = 1.0f;
    [SerializeField] private float maxTime = 60.0f;
    private float curFrame = 1.0f;


    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        int playerNum = UIManager.Instance.playerNum;

        scoreTexts = new Score[playerNum];
        feverTexts = new GameObject[playerNum];
        feverCanvases = new Canvas[playerNum];

        // Score
        for (int i = 0; i < playerNum; i++)
        {
            float distance = 1600f * i;
            GameObject gameObject = Instantiate(scoreObject);
            gameObject.transform.SetParent(transform);
            RectTransform Rect = gameObject.GetComponent<RectTransform>();
            Rect.transform.localPosition = scorePosition;
            Rect.transform.localPosition = new Vector3(scorePosition.x + distance, scorePosition.y, 0);

            scoreTexts[i] = gameObject.GetComponent<Score>();
        }

        // FeverText
        for (int i = 0; i < playerNum; i++)
        {
            float distance = 1000f * i;

            GameObject gameObject = Instantiate(feverText);
            gameObject.transform.SetParent(transform);
            RectTransform Rect = gameObject.GetComponent<RectTransform>();
            Rect.transform.localPosition = new Vector3(feverTextPosition.x + distance, feverTextPosition.y, 0);

            feverCanvases[i] = gameObject.GetComponent<Canvas>();
            feverCanvases[i].enabled = false;

            feverTexts[i] = gameObject;
            
        }

        // CountDown
        {
            countDown = countDownObject.GetComponent<CountDown>();
        }

        // Timer
        {
            timer = timerObject.GetComponent<TimeProgress>();
        }

        UIManager.Instance.maxTime = maxTime;
        UIManager.Instance.curTime = maxTime;
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
                if (UIManager.Instance.curTime < 0)
                {
                    StopGame();
                    // InGame ������߰� ���ȭ�� ����
                }
                else
                {
                    DecreaseTime();
                    //UIManager.Instance.curTime--;
                    UIManager.Instance.curTime = NoteManager.Instance.noteTimeInfo.PlayTime - NoteManager.Instance.curTime;
                }
                curFrame = 0f;
            }
        }
    }

    private void StopGame()
    {
        
    }

    public override void Activate()
    {
        base.Activate();

        countDown.Activate();
        // Canvas enabled true�� �����غ�
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
}
