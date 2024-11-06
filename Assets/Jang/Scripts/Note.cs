using UnityEngine;

public class Note : MonoBehaviour
{
    NoteTimeInfo noteTimeInfo;
    private float curTime;
    private int level = 0;
    private bool isFever = false;
    private float lastCheckTime = 0;

    public void SetLevel(int lv)
    {
        level = lv;
    }
    public void SetFever(bool b)
    {
        curTime = noteTimeInfo.FeverStartTime;
        isFever = b;
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
        gameObject.SetActive(false);

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
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        curTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
    }
}
