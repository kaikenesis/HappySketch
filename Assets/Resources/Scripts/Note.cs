using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Note : MonoBehaviour
{
    private NoteManager noteManager;
    private float[] totalTime;
    private float[] perfectTime;
    private float[] goodTime;
    private float curTime;
    NoteTimeInfo noteTimeInfo;
    public int level;
    
    public void SetNoteTimeInfo(NoteTimeInfo noteTimeInfo)
    {
        this.noteTimeInfo = noteTimeInfo;
    }

    public int Check()
    {
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
        level = 0;
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
