using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : Singleton<NoteManager>
{
    [SerializeField] private NoteTimeInfo noteTimeInfo;
    [SerializeField] private GameObject noteEffect;

    private int level = 0;
    private int score = 0;
    private int outCircleWidth = 300;
    private float curTime = 0;

    private bool canEnable = true;
    private bool isFever = false;
    [SerializeField]private bool isPlay = false;

    Coroutine enableNote = null;
    Coroutine disableNote = null;
    Coroutine waitRecreate = null;

    private List<GameObject> notes = new();
    private List<GameObject> inCircleNotes = new();
    Vector2[] positions = { new(-800, 80), new(-200, 80), new(-800, -400), new(-200, -400),
                            new(200, 80), new(800, 80), new(200, -400), new(800, -400)};
    private Dictionary<KeyCode, int> keyDict = new();
    private readonly KeyCode[] keyCodes = { KeyCode.A, KeyCode.S, KeyCode.Z, KeyCode.X, KeyCode.J, KeyCode.K, KeyCode.N, KeyCode.M };

    void Start()
    {
        SetKeys();
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

        if (isFever)
        {
            for (int i = 0; i < inCircleNotes.Count; i++)
            {
                CircleGraphic inCircleCG = inCircleNotes[i].GetComponent<CircleGraphic>();
                
                inCircleCG.color = new Color(Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f);
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void SetKeys()
    {        
        keyDict.Add(KeyCode.A, 0);
        keyDict.Add(KeyCode.S, 1);
        keyDict.Add(KeyCode.Z, 2);
        keyDict.Add(KeyCode.X, 3);
        keyDict.Add(KeyCode.J, 4);
        keyDict.Add(KeyCode.K, 5);
        keyDict.Add(KeyCode.N, 6);
        keyDict.Add(KeyCode.M, 7);
    }

    public void SetGameStart()
    {
        isPlay = true;
        curTime = 0;
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

            //  1 2 3 : 40 40 20
            //  총 점수 같음
            //  25퍼 50퍼 80퍼
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
        enableNote = null;
        disableNote = StartCoroutine(DisableNote());
    }

    IEnumerator DisableNote()
    {
        Debug.Log("턴 종료");
        for (int i = 0; i < notes.Count; i++)
        {
            inCircleNotes[i].SetActive(false);
        }
        disableNote = null;
        yield return waitRecreate = StartCoroutine(WaitRecreate());
    }

    IEnumerator WaitRecreate()
    {
        float rand = Random.Range(noteTimeInfo.MinRecreateTime, noteTimeInfo.MaxRecreateTime);
        Debug.Log($"재생성 시간 : {rand}");
        yield return new WaitForSeconds(rand);
        waitRecreate = null;
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
            //color
            inCircleCG.color = new Color32(185, 197, 238, 255);
            inCircleNotes.Add(insideCircle);

            GameObject obj = new GameObject();
            obj.name = "note" + (i + 1);
            obj.transform.SetParent(transform);
            obj.transform.localPosition = positions[i];
            obj.transform.SetParent(insideCircle.transform);
            notes.Add(obj);

            Note note = obj.AddComponent<Note>();
            note.SetNoteTimeInfo(noteTimeInfo);
            CircleGraphic cg = obj.AddComponent<CircleGraphic>();
            //color
            cg.color = new Color32(238,74,74,255);
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
        foreach (KeyCode key in keyCodes)
        {
            if (Input.GetKeyDown(key))
            {
                Check(keyDict[key]);
                return;
            }
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
            if (i < 4)
            {
                if (score != noteTimeInfo.BadScore)
                {
                    GameController.Instance.MoveupPlayer(1, 1);
                    // 스코어도 준다
                }
                return;
            }
            if (score != noteTimeInfo.BadScore)
            {
                GameController.Instance.MoveupPlayer(2, 1);
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

            if (enableNote != null)
                StopCoroutine(enableNote);
            if (disableNote != null)
                StopCoroutine(disableNote);
            if (waitRecreate != null)
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
