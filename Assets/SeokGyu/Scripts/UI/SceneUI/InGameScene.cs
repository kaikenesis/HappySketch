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
    private int playerNum = 2;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
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

            //for (int j = 0; j < nodesPosition.Length; j++)
            //{
            //    GameObject nodeObject = Instantiate(decideNode);
            //    nodeObject.transform.SetParent(transform);
            //    RectTransform nodeRect = nodeObject.GetComponent<RectTransform>();
            //    nodeRect.sizeDelta = new Vector2(nodeWidth, nodeHeight);
            //    nodeRect.transform.localPosition = new Vector3(nodesPosition[j].x + distance, nodesPosition[j].y, 0);
            //}
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

    public void InCreaseScore(int score1, int score2)
    {
        scores[0].SetText(score1);
        scores[1].SetText(score2);
    }

    public void ActivateFeverTime()
    {
        for(int i =0;i< feverTexts.Length;i++)
        {
            feverTexts[i].GetComponent<Canvas>().enabled = true;
        }
    }
}
