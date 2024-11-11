using TMPro;
using UnityEngine;

public class CountDown : BaseScene
{
    [SerializeField]
    private GameObject textObject;
    private TextMeshProUGUI countText;
    [SerializeField]
    private int maxCount = 3;
    private int curCount;
    public bool isActive = false;
    private float timer = 0f;
    private float waitngTime = 1f;

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        countText = textObject.GetComponent<TextMeshProUGUI>();
        if (countText != null)
        {
            curCount = maxCount;
            countText.text = curCount.ToString();
        }
    }

    private void Update()
    {
        CountTime();
    }

    private void CountTime()
    {
        if (isActive == false) return;

        timer += Time.deltaTime;
        if (timer >= waitngTime)
        {
            curCount--;
            if (curCount <= 0)
            {
                isActive = false;
                GetComponent<Canvas>().enabled = false;
                NoteManager.instance.SetGameStart();
            }
            else
            {
                countText.text = curCount.ToString();
            }
            timer = 0f;
        }
    }

    public override void Activate()
    {
        base.Activate();

        transform.SetAsLastSibling();
        isActive = true;
        curCount = maxCount;
        countText.text = curCount.ToString();
    }
}
