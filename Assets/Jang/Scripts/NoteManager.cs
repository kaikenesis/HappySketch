using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private List<GameObject> notes = new();
    [SerializeField] NoteTimeInfo noteTimeInfo;
    
    private int level = 0;
    private int score = 0;
    private int curScore = 0;
    private int correctCount = 0;
    private float curTime = 0;
    
    private bool canEnable = true;
    private bool isFever = false;
    private bool isPlay = true; // false로 하다가 set하는 방식으로 게임 시작

    Coroutine enableNote = null;
    Coroutine disableNote = null;
    Coroutine waitRecreate = null;

    void Start()
    {
        for(int i = 0; i<4; i++)
        {
            GameObject obj = new GameObject();
            obj.name = "note" + (i+1);
            obj.AddComponent<Note>();
            obj.GetComponent<Note>().SetNoteTimeInfo(noteTimeInfo);
            notes.Add(obj);
        }
    }

    void Update()
    {
        if (!isPlay)
            return;

        curTime += Time.deltaTime;      
        
        CheckNotes();
        CheckGameEnd();

        if (!isFever)
            CheckFever();

        if (canEnable && !isFever)
        {
            enableNote = StartCoroutine(EnableNote());
        }
    }

    public void SetLevel(int lv)
    {
        level = lv;
        for(int i = 0; i < notes.Count; i++)
        {
            notes[i].GetComponent<Note>().SetLevel(lv);
        }
    }

    IEnumerator EnableNote()
    {
        canEnable = false;
        Debug.Log("생성");
        for (int i = 0; i < notes.Count; i++)
        {
            float rand = Random.Range(0, 100);
            if (rand >= 50)
                notes[i].SetActive(true);
            if (i == 3)
            {
                if(notes[i-1].activeSelf == true)
                    notes[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(noteTimeInfo.TotalTime[level]);
        disableNote = StartCoroutine(DisableNote());
    }

    IEnumerator DisableNote()
    {
        Debug.Log(correctCount);
        // 여기서 count를 넘겨줘?
        correctCount = 0;
        Debug.Log("턴 종료");
        for (int i = 0; i < notes.Count; i++)
        {
            notes[i].SetActive(false);
        }
        yield return waitRecreate = StartCoroutine(WaitRecreate());
    }

    IEnumerator WaitRecreate()
    {
        float rand = Random.Range(noteTimeInfo.MinRecreateTime, noteTimeInfo.MaxRecreateTime);
        Debug.Log($"재생성 시간 : {rand}");
        yield return new WaitForSeconds(rand);
        canEnable = true;
    }

    void CheckNotes()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (notes[0].activeSelf ==  true)
            { 
                score = notes[0].GetComponent<Note>().Check();
                curScore += score;
                if (score != noteTimeInfo.BadScore)
                {
                    correctCount++;
                }
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (notes[1].activeSelf == true)
            {    
                score = notes[1].GetComponent<Note>().Check();
                curScore += score;
                if (score != noteTimeInfo.BadScore)
                {
                    correctCount++;
                }
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (notes[2].activeSelf == true)
            {    
                score = notes[2].GetComponent<Note>().Check();
                curScore += score;
                if (score != noteTimeInfo.BadScore)
                {
                    correctCount++;
                }
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (notes[3].activeSelf == true)
            {    
                score = notes[3].GetComponent<Note>().Check();
                curScore += score;
                if (score != noteTimeInfo.BadScore)
                {
                    correctCount++;
                }
            }
            return;
        }        
    }

    void CheckFever()
    {
        if(curTime >= noteTimeInfo.FeverStartTime)
        {
            isFever = true;
            Debug.Log("FEVER!");
            for (int i = 0; i < notes.Count; i++)
            {
                notes[i].GetComponent<Note>().SetFever(true);
            }

            StopCoroutine(enableNote);
            StopCoroutine(disableNote);
            StopCoroutine(waitRecreate);

            for (int i = 0; i < notes.Count; i++)
            {
                notes[i].SetActive(true);
            }
        }
    }

    void CheckGameEnd()
    {
        if(curTime >= noteTimeInfo.PlayTime)
        {
            Debug.Log("게임 종료");
            isPlay = false;
            for (int i = 0; i < notes.Count; i++)
            {
                notes[i].GetComponent<Note>().SetFever(false);
            }
            for (int i = 0; i < notes.Count; i++)
            {
                notes[i].SetActive(false);
            }
        }
    }

    public void SetGameStart()
    {
        isPlay = true;
    }
}
