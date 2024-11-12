using UnityEngine;

public class InGameScene : BaseScene
{
    [SerializeField] private GameObject score;
    [SerializeField] private Vector2 scorePosition;
    [SerializeField] private GameObject feverText;
    [SerializeField] private Vector2 feverTextPosition;
    [SerializeField] private GameObject decideNode;
    [SerializeField] private Vector2[] nodesPosition;
    [SerializeField] private float nodeWidth;
    [SerializeField] private float nodeHeight;
    [SerializeField] private GameObject countDown;
    [SerializeField] private GameObject timer;
    private Score[] scores;
    private GameObject[] feverTexts;
    

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        int playerNum = UIManager.Instance.playerNum;

        scores = new Score[playerNum];
        feverTexts = new GameObject[playerNum];

        // Score
        for (int i = 0; i < playerNum; i++)
        {
            float distance = 1600f * i;
            GameObject scoreObject = Instantiate(score);
            scoreObject.transform.SetParent(transform);
            RectTransform scoreRect = scoreObject.GetComponent<RectTransform>();
            scoreRect.transform.localPosition = scorePosition;
            scoreRect.transform.localPosition = new Vector3(scorePosition.x + distance, scorePosition.y, 0);

            scores[i] = scoreObject.GetComponent<Score>();
        }

        // FeverText
        for (int i = 0; i < playerNum; i++)
        {
            float distance = 1000f * i;

            GameObject feverTextObject = Instantiate(feverText);
            feverTextObject.transform.SetParent(transform);
            RectTransform feverTextRect = feverTextObject.GetComponent<RectTransform>();
            feverTextRect.transform.localPosition = new Vector3(feverTextPosition.x + distance, feverTextPosition.y, 0);
            feverTextObject.GetComponent<Canvas>().enabled = false;

            feverTexts[i] = feverTextObject;
        }

        // CountDown
        GameObject countDownObject = Instantiate(countDown);
        countDownObject.transform.SetParent(transform);
    }

    public override void Activate()
    {
        base.Activate();

        countDown.GetComponent<CountDown>().Activate();
        // Canvas enabled true시 실행준비
    }

    public void DecreaseTime()
    {
        timer.GetComponent<TimeProgress>().Decrease();
    }

    public void SetScore(int playerNum, int score)
    {
        scores[playerNum].SetText(score);
    }

    public void ActivateFeverTime()
    {
        for(int i =0;i< feverTexts.Length;i++)
        {
            feverTexts[i].GetComponent<Canvas>().enabled = true;
        }
    }
}
