using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private GameObject textObject;
    private TextMeshProUGUI text;
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

    private void Init()
    {
        text = textObject.GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            curCount = maxCount;
            text.text = curCount.ToString();
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
            }
            else
            {
                text.text = curCount.ToString();
            }
            timer = 0f;
        }
    }

    public void Activation()
    {
        isActive = true;
        curCount = maxCount;
        text.text = curCount.ToString();
    }
}
