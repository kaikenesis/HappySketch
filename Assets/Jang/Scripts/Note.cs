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
    private CircleGraphic circleGraphic;
    public void SetLevel(int lv)
    {
        level = lv;
    }
    public void SetFever(bool b)
    {
        isFever = b;
        curTime = noteTimeInfo.FeverStartTime;
        lastCheckTime = 0;
        if (rectTran == null)
            rectTran = GetComponent<RectTransform>();
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
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
                return -1;
            }
            SetLastCheckTime();
            return noteTimeInfo.PerfectScore;
        }

        transform.parent.gameObject.SetActive(false);

        if (curTime > noteTimeInfo.TotalTime[level] / 2 + noteTimeInfo.PerfectTime[level] + noteTimeInfo.GoodTime[level])
        {
            return noteTimeInfo.BadScore;
        }
        else if (curTime > noteTimeInfo.TotalTime[level] / 2 + noteTimeInfo.PerfectTime[level])
        {
            return noteTimeInfo.GoodScore;
        }
        else if (curTime < noteTimeInfo.TotalTime[level] / 2 - noteTimeInfo.PerfectTime[level] - noteTimeInfo.GoodTime[level])
        {
            return noteTimeInfo.BadScore;
        }
        else if (curTime < noteTimeInfo.TotalTime[level] / 2 - noteTimeInfo.PerfectTime[level])
        {
            return noteTimeInfo.GoodScore;
        }

        return noteTimeInfo.PerfectScore;
    }

    private void Start()
    {
        rectTran = GetComponent<RectTransform>();
        circleGraphic = GetComponent<CircleGraphic>();
    }
    void OnEnable()
    {
        curTime = 0;
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (!isFever)
            ShrinkCircle();
    }

    void ShrinkCircle()
    {
        float progress = Mathf.Clamp01(1 - (curTime / noteTimeInfo.TotalTime[level]));
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, noteWidth * progress);
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, noteWidth * progress);
    }
}
