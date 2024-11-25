using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    private Canvas canvas;
    [SerializeField] private int maxTime = 3;
    private int curTime;
    private float curFrame = 0f;
    private float waitngTime = 1f;
    private bool isActive;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        canvas = GetComponent<Canvas>();
        curTime = maxTime;
        countText.text = curTime.ToString();
        isActive = false;
    }

    private void Update()
    {
        CountTime();
    }

    private void CountTime()
    {
        if (isActive == false) return;
        
        curFrame += Time.deltaTime;
        if (curFrame >= waitngTime)
        {
            if (curTime <= 0)
            {
                isActive = false;
                canvas.enabled = false;
                UIManager.Instance.bPlayGame = true;
                NoteManager.instance.SetGameStart();
                SoundManager.PlayBGM(ConstStrings.BGM_GAME);
            }
            else
            {
                SoundManager.PlaySFX(ConstStrings.SFX_COUNTDOWN);
                countText.text = curTime.ToString();
                curTime--;
            }
            curFrame = 0f;
        }
    }

    public void Activate()
    {
        SoundManager.StopBGM();

        canvas.enabled = true;
        isActive = true;
        curTime = maxTime;
        curFrame = 1.0f;
        countText.text = curTime.ToString();
    }
}
