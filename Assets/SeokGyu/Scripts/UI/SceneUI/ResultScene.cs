using UnityEngine;

public class ResultScene : BaseScene
{
    [SerializeField] private GameObject resultScoreObject;
    [SerializeField] private float distance = 500;
    private ResultScore[] resultScores;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        int playerNum = UIManager.Instance.playerNum;
        resultScores = new ResultScore[playerNum];

        for (int i = 0; i < playerNum; i++)
        {
            distance *= -1;
            GameObject gameObject = Instantiate(resultScoreObject);
            gameObject.transform.SetParent(transform);
            gameObject.transform.localPosition = new Vector3(distance, 0, 0);
            resultScores[i] = gameObject.GetComponent<ResultScore>();
        }
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public void SetWinner(int playerNum)
    {
        for (int i = 0; i < UIManager.Instance.playerNum; i++)
        {
            if(i != playerNum)
                resultScores[i].SetText(false, i);
            else
                resultScores[i].SetText(true, i);
        }
    }
}
