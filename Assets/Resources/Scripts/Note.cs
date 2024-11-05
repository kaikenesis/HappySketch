using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Note : MonoBehaviour
{
    private float[] totalTime;
    private float[] perfectTime;
    private float[] goodTime;
    private float curTime;

    public int level;
    [SerializeField] NoteTimeInfo noteTimeInfo;
    
    public int Check()
    {
        Debug.Log(curTime);
        if (curTime > totalTime[level] / 2 + perfectTime[level] + goodTime[level])
        {
            Debug.Log("Bad");
            return 0;
        }
        else if (curTime > totalTime[level] / 2 + perfectTime[level])
        {
            Debug.Log("Good");
            return 5;
        }
        else if (curTime < totalTime[level] / 2 - perfectTime[level] - goodTime[level])
        {
            Debug.Log("Bad");
            return 0;
        }
        else if (curTime < totalTime[level] / 2 - perfectTime[level])
        {
            Debug.Log("Good");
            return 5;
        }        

        Debug.Log("Perfect");
        return 10;
    }

    private void Start()
    {
        level = 0;        
    }
    void OnEnable()
    {
        curTime = 0;
        Debug.Log("enable");
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A))
        {
            Check();
            gameObject.SetActive(false);
        }
    }
}
