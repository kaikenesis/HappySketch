using UnityEngine;

public class Note : MonoBehaviour
{
    NoteTimeInfo noteTimeInfo;
    private float curTime;
    private int level = 0;
    private bool isFever = false;
    private float lastCheckTime = 0;
    private float noteWidth = 300;
    private RectTransform rectTran;

    public void SetLevel(int lv)
    {
        level = lv;
    }
    public void SetFever(bool b)
    {
        curTime = noteTimeInfo.FeverStartTime;
        isFever = b;
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, noteWidth);
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, noteWidth);
    }

    void SetLastCheckTime()
    {
        lastCheckTime = curTime;
    }

    public void SetNoteTimeInfo(NoteTimeInfo noteTimeInfo)
    {
        this.noteTimeInfo = noteTimeInfo;
    }

    public int Check()
    {
        //ÇÇ¹ö
        if (isFever)
        {
            if (curTime - lastCheckTime < noteTimeInfo.FeverCheckTime)
            {
                Debug.Log(curTime);
                return 0;
            }
            SetLastCheckTime();
            Debug.Log("Perfect");
            return noteTimeInfo.PerfectScore;
        }

        Debug.Log(curTime);
        transform.parent.gameObject.SetActive(false);
        //gameObject.SetActive(false);

        if (curTime > noteTimeInfo.TotalTime[level] / 2 + noteTimeInfo.PerfectTime[level] + noteTimeInfo.GoodTime[level])
        {
            Debug.Log("Bad");
            return noteTimeInfo.BadScore;
        }
        else if (curTime > noteTimeInfo.TotalTime[level] / 2 + noteTimeInfo.PerfectTime[level])
        {
            Debug.Log("Good");
            return noteTimeInfo.GoodScore;
        }
        else if (curTime < noteTimeInfo.TotalTime[level] / 2 - noteTimeInfo.PerfectTime[level] - noteTimeInfo.GoodTime[level])
        {
            Debug.Log("Bad");
            return noteTimeInfo.BadScore;
        }
        else if (curTime < noteTimeInfo.TotalTime[level] / 2 - noteTimeInfo.PerfectTime[level])
        {
            Debug.Log("Good");
            return noteTimeInfo.GoodScore;
        }

        Debug.Log("Perfect");
        return noteTimeInfo.PerfectScore;
    }

    private void Start()
    {
        rectTran = GetComponent<RectTransform>();
    }
    void OnEnable()
    {
        curTime = 0;
        noteWidth = 300;
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if(!isFever)
            ShrinkImage();
    }

    void ShrinkImage()
    {
        float progress = Mathf.Clamp01(1 - (curTime / noteTimeInfo.TotalTime[level]));
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, noteWidth * progress);
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, noteWidth * progress);
    }
}
