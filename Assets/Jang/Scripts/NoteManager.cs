using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    private List<GameObject> notes = new();
    private List<GameObject> inCircleNotes = new();
    [SerializeField] private NoteTimeInfo noteTimeInfo;
    [SerializeField] private GameObject noteEffect;

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

    Vector2[] positions = { new(-800, 80), new(-200, 80), new(-800, -400), new(-200, -400),
                            new(200, 80), new(800, 80), new(200, -400), new(800, -400)};
    private int outCircleWidth = 300;

    void Start()
    {
        RectTransform parentRect = transform.parent.GetComponent<RectTransform>();
        parentRect.localScale = Vector3.one;
        GenerateNotes();
        noteEffect.transform.localScale = new Vector3(100,100,100);
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

    public void SetGameStart()
    {
        isPlay = true;
    }

    public void SetLevel(int lv)
    {
        level = lv;
        for (int i = 0; i < notes.Count; i++)
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
            if (rand >= 30)
            { 
                inCircleNotes[i].SetActive(true);
            }
            if (i == 3 || i == 7)
            {
                if(inCircleNotes[i-1].activeSelf == true)
                { 
                    inCircleNotes[i].SetActive(false);
                }
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
            inCircleNotes[i].SetActive(false);
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

    void GenerateNotes()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject insideCircle = new GameObject();
            insideCircle.name = "inCircle note" + (i + 1);
            insideCircle.transform.SetParent(transform);
            insideCircle.transform.localPosition = positions[i];
            CircleGraphic inCircleCG = insideCircle.AddComponent<CircleGraphic>();

            RectTransform childRectTran = insideCircle.GetComponent<RectTransform>();
            childRectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, outCircleWidth / 2);
            childRectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, outCircleWidth / 2);
            inCircleCG.color = Color.blue;
            inCircleNotes.Add(insideCircle);

            GameObject obj = new GameObject();
            obj.name = "note" + (i + 1);
            obj.transform.SetParent(transform);
            obj.transform.localPosition = positions[i];
            obj.transform.SetParent(insideCircle.transform);
            notes.Add(obj);

            Note note = obj.AddComponent<Note>();
            note.SetNoteTimeInfo(noteTimeInfo);
            note.SetEffect(noteEffect);
            CircleGraphic cg = obj.AddComponent<CircleGraphic>();
            cg.color = Color.red;
            cg.SetMode(CircleGraphic.Mode.Edge);
            cg.SetEdgeThickness(10);

            RectTransform rectTran = obj.GetComponent<RectTransform>();
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, outCircleWidth);
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, outCircleWidth);

            insideCircle.SetActive(false);
        }
    }

    void CheckNotes()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Check(0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Check(1);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Check(2);
            return;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Check(3);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            Check(4);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Quote))
        {
            Check(5);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            Check(6);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            Check(7);
            return;
        }
    }

    void Check(int i)
    {
        Note note = notes[i].GetComponent<Note>();
        Vector3 createPos = note.transform.position;
        Instantiate(noteEffect, createPos, Quaternion.identity, transform.parent);
        if (inCircleNotes[i].activeSelf == true)
        {
            score = note.Check();
            curScore += score;
            if (score != noteTimeInfo.BadScore)
            {
                correctCount++;
            }
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
                inCircleNotes[i].SetActive(true);
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
                inCircleNotes[i].SetActive(false);
            }
        }
    }

}
