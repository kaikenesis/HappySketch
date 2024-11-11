using UnityEngine;

public class ResultScene : BaseScene
{
    [SerializeField] private GameObject resultScoreObject;
    [SerializeField] private float distance = 500;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        for(int i =0;i< UIManager.Instance.playerNum;i++)
        {
            distance *= -1;
            GameObject gameObject = Instantiate(resultScoreObject);
            gameObject.transform.SetParent(transform);
            gameObject.transform.localPosition = new Vector3(distance, 0, 0);
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

    public void SetResult()
    {

    }
}
