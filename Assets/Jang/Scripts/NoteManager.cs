//using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : Singleton<NoteManager>
{
    [SerializeField] public NoteTimeInfo noteTimeInfo;
    [SerializeField] private GameObject noteEffect;
    [SerializeField] private TMP_FontAsset fontCafe24;
    [SerializeField] private Material feverNoteMat;
    private Material originalFontMat;

    private int level = 0;
    private int score = 0;
    private int circleWidth = 150;
    public float curTime = 0;
    private float noteTime = 0;

    private bool canEnable = true;
    private bool isFever = false;
    private bool isPlay = false;
    private bool isAboutToFever = false;

    Coroutine enableNote = null;
    Coroutine disableNote = null;
    Coroutine waitRecreate = null;

    private List<GameObject> notes = new();
    private List<GameObject> inCircleNotes = new();
    private List<GameObject> numberText = new();
    private List<GameObject> resultText = new();
    private List<GameObject> feverNotes = new();
    Vector2[] positions = { new(-800, 80), new(-200, 80), new(-800, -400), new(-200, -400),
                            new(200, 80), new(800, 80), new(200, -400), new(800, -400)};
    private Dictionary<KeyCode, int> keyDict = new();
    private readonly KeyCode[] keyCodes = { KeyCode.A, KeyCode.S, KeyCode.Z, KeyCode.X, KeyCode.J, KeyCode.K, KeyCode.N, KeyCode.M };

    Sprite feverKnob = null;
    void Start()
    {
        feverKnob = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
        SetKeys();
        GenerateNotes();
        noteEffect.transform.localScale = new Vector3(100,100,100);
        originalFontMat = numberText[0].GetComponent<TextMeshProUGUI>().fontMaterial;
    }

    void Update()
    {
        if (!isPlay)
            return;

        curTime += Time.deltaTime;
        noteTime += Time.deltaTime;

        CheckNotes();
        CheckGameEnd();

        SetNumText();

        if (!isFever)
            CheckFever();

        if (canEnable && !isFever)
        {
            enableNote = StartCoroutine(EnableNote());
        }

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
        curTime = 0;
        isFever = false;
        isAboutToFever = false;
        canEnable = true;
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
        noteTime = 0;
        float rand = Random.Range(0, 100);
        int totalNoteCount = 0;
        int curNoteCount = 0;
        int range = 0;
        //  1 2 3 : 40 40 20
        //  총 점수 같음
        //  25퍼 50퍼 70퍼
        if (rand < 40)
        {
            totalNoteCount = 1;
            range = 25;
        }
        else if (rand < 80)
        {
            totalNoteCount = 2;
            range = 50;
        }
        else
        {
            totalNoteCount = 3;
            range = 70;
        }

        if (totalNoteCount == 3)
        {
            inCircleNotes[0].SetActive(true);
            inCircleNotes[1].SetActive(true);
            float random = Random.Range(0, 100);
            if (random < 50)
                inCircleNotes[2].SetActive(true);
            else
                inCircleNotes[3].SetActive(true);

            inCircleNotes[4].SetActive(true);
            inCircleNotes[5].SetActive(true);
            random = Random.Range(0, 100);
            if (random < 50)
                inCircleNotes[6].SetActive(true);
            else
                inCircleNotes[7].SetActive(true);
        }
        else
        {
            for (int i = 0; i < notes.Count / 2; i++)
            {
                float random = Random.Range(0, 100);
                if (random < range && inCircleNotes[i].activeSelf == false)
                {
                    inCircleNotes[i].SetActive(true);
                    curNoteCount++;
                }

                if (inCircleNotes[2].activeSelf == true && inCircleNotes[3].activeSelf == true)
                {
                    if (random < 50)
                        inCircleNotes[2].SetActive(false);
                    else
                        inCircleNotes[3].SetActive(false);
                    curNoteCount--;
                }

                if (curNoteCount == totalNoteCount)
                    break;
                if (i == 3 && curNoteCount != totalNoteCount)
                    i = 0;
            }

            curNoteCount = 0;
            for (int i = notes.Count / 2; i < notes.Count; i++)
            {
                float random = Random.Range(0, 100);
                if (random < range && inCircleNotes[i].activeSelf == false)
                {
                    inCircleNotes[i].SetActive(true);
                    curNoteCount++;
                }

                if (inCircleNotes[6].activeSelf == true && inCircleNotes[7].activeSelf == true)
                {
                    if (random < 50)
                        inCircleNotes[6].SetActive(false);
                    else
                        inCircleNotes[7].SetActive(false);
                    curNoteCount--;
                }

                if (curNoteCount == totalNoteCount)
                    break;
                if (i == 7 && curNoteCount != totalNoteCount)
                    i = notes.Count / 2;
            }
        }
        yield return new WaitForSeconds(noteTimeInfo.TotalTime[level]);
        enableNote = null;
        disableNote = StartCoroutine(DisableNote());
    }

    IEnumerator DisableNote()
    {
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
        yield return new WaitForSeconds(rand);
        waitRecreate = null;
        canEnable = true;
    }

    void GenerateNotes()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject insideCircle = new GameObject();
            insideCircle.name = "InCircle note" + (i + 1);
            insideCircle.transform.SetParent(transform);
            insideCircle.transform.localPosition = positions[i];
            CircleGraphic inCircleCG = insideCircle.AddComponent<CircleGraphic>();

            RectTransform childRectTran = insideCircle.GetComponent<RectTransform>();
            childRectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, circleWidth);
            childRectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, circleWidth);
            inCircleCG.color = new Color32(0xff, 0xa0, 0x7a, 255);
            inCircleNotes.Add(insideCircle);
            insideCircle.SetActive(false);

            GameObject OutCircleNote = new GameObject();
            OutCircleNote.name = "OutCircle Note" + (i + 1);
            OutCircleNote.transform.SetParent(transform);
            OutCircleNote.transform.localPosition = positions[i];
            OutCircleNote.transform.SetParent(insideCircle.transform);
            notes.Add(OutCircleNote);

            Note note = OutCircleNote.AddComponent<Note>();
            note.SetNoteTimeInfo(noteTimeInfo);
            CircleGraphic cg = OutCircleNote.AddComponent<CircleGraphic>();
            cg.color = new Color32(238,74,74,255);
            cg.SetMode(CircleGraphic.Mode.Edge);
            cg.SetEdgeThickness(10);

            RectTransform rectTran = OutCircleNote.GetComponent<RectTransform>();
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, circleWidth);
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, circleWidth);

            GameObject numText = new();
            TextMeshProUGUI numTextUGUI = numText.AddComponent<TextMeshProUGUI>();
            numText.name = "numberText" + (i + 1);
            numText.transform.SetParent(transform);
            numText.transform.localPosition = positions[i];
            numText.transform.SetParent(insideCircle.transform);
            numTextUGUI.text = (i + 1).ToString();
            numTextUGUI.fontSize = 70;
            numTextUGUI.alignment = TextAlignmentOptions.Center;
            numTextUGUI.font = fontCafe24;
            numTextUGUI.color = new Color32(0x00, 0x00, 0x00, 255);
            numberText.Add(numText);

            GameObject resText = new();
            TextMeshProUGUI resTextUGUI = resText.AddComponent<TextMeshProUGUI>();
            resText.name = "resultText" + (i + 1);
            resText.transform.SetParent(transform);
            resText.transform.localPosition = new Vector2(positions[i].x, positions[i].y - 120);
            resTextUGUI.fontSize = 32;
            resTextUGUI.alignment = TextAlignmentOptions.Center;
            resTextUGUI.font = fontCafe24;
            resultText.Add(resText);

            GameObject feverNote = new();
            feverNote.name = "feverNote";
            Image feverImage = feverNote.AddComponent<Image>();
            RectTransform feverRect = feverImage.GetComponent<RectTransform>();
            feverRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, circleWidth + 10);
            feverRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, circleWidth + 10);
            feverNote.transform.SetParent(transform);
            feverNote.transform.localPosition = new Vector3( positions[i].x, positions[i].y, -1);
            feverImage.sprite = feverKnob;
            feverImage.material = feverNoteMat;
            feverNotes.Add(feverNote);
            feverNote.SetActive(false);

            GameObject feverOutline = new();
            CircleGraphic feverCircle = feverOutline.AddComponent<CircleGraphic>();
            RectTransform feverOutlineRect = feverOutline.GetComponent<RectTransform>();
            feverOutline.name = "FeverOutline";
            feverOutlineRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, circleWidth);
            feverOutlineRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, circleWidth);
            feverOutline.transform.SetParent(transform);
            feverOutline.transform.localPosition = new Vector3(positions[i].x, positions[i].y, -1);
            feverOutline.transform.SetParent(feverNote.transform);
            feverCircle.SetMode(CircleGraphic.Mode.Edge);
            feverCircle.SetEdgeThickness(10);

            GameObject feverText = new();
            TextMeshProUGUI feverTextUGUI = feverText.AddComponent<TextMeshProUGUI>();
            feverText.name = "numberText" + (i + 1);
            feverText.transform.SetParent(transform);
            feverText.transform.localPosition = new Vector3(positions[i].x, positions[i].y, -1);
            feverText.transform.SetParent(feverNote.transform);
            feverTextUGUI.alignment = TextAlignmentOptions.Center;
            feverTextUGUI.font = fontCafe24;
            feverTextUGUI.color = new Color32(0xff, 0xff, 0xff, 255);
            feverTextUGUI.outlineColor = Color.red;
            feverTextUGUI.text = "FEVER";
            feverTextUGUI.fontSize = 34;

        }
    }
    
    void SetNumText()
    {
        for(int i = 0; i < numberText.Count; i++)
        {
            TextMeshProUGUI numText = numberText[i].GetComponent<TextMeshProUGUI>();
            if (noteTimeInfo.TotalTime[level] / 2 - noteTime < 0)
            {
                numText.text = null;
                
            }
            else
                numText.text = (noteTimeInfo.TotalTime[level] / 2 - noteTime + 1).ToString("F0");
        }
    }
    
    void CheckNotes()
    {
        foreach (KeyCode key in keyCodes)
        {
            if (Input.GetKeyDown(key))
            {
                Check(keyDict[key]);
            }
        }
    }

    void Check(int i)
    {
        Note note = notes[i].GetComponent<Note>();
        Vector3 createPos = note.transform.position;
        Instantiate(noteEffect, createPos, Quaternion.identity, transform.parent);
        SoundManager.PlaySFX(AudioNameTag.SFX_NOTEHIT_DEFAULT);

        if (inCircleNotes[i].activeSelf == true)
        {
            score = note.Check();
            if (i < 4)
            {
                if (score != noteTimeInfo.BadScore && score != -1)
                {
                    GameController.Instance.MoveupPlayer(1);
                    UIManager.Instance.uiDirector.IncreaseScore(1, score);
                }
            }
            else
            {
                if (score != noteTimeInfo.BadScore && score != -1)
                {
                    GameController.Instance.MoveupPlayer(2);
                    UIManager.Instance.uiDirector.IncreaseScore(2, score);
                }
            }
            StartCoroutine(SetResultText(i, score));
        }
    }
    
    IEnumerator SetResultText(int idx, int score)
    {
        TextMeshProUGUI resTextUGUI = resultText[idx].GetComponent<TextMeshProUGUI>();
        resultText[idx].SetActive(true);
        resTextUGUI.outlineColor = new Color32(0x00, 0x00, 0x00, 255);

        if (score == noteTimeInfo.BadScore)
        {
            resTextUGUI.text = "Bad";
            resTextUGUI.color = new Color32(0xc0, 0xc0, 0xc0, 255);
        }
        else if (score == noteTimeInfo.GoodScore)
        {
            resTextUGUI.text = "Good";
            resTextUGUI.color = new Color32(0x94, 0xc1, 0x36, 255);
        }
        else if (score == noteTimeInfo.PerfectScore)
        {
            resTextUGUI.text = "Perfect";
            resTextUGUI.color = new Color32(0xff, 0xd7, 0x00, 255);
        }
        else
            yield return null;

        if (isFever)
            yield return new WaitForSeconds(0.5f);
        else
            yield return new WaitForSeconds(1.0f);

        resultText[idx].SetActive(false);
    }

    void CheckFever()
    {
        if (!isAboutToFever && curTime >= noteTimeInfo.FeverStartTime - 0.8f)
        {
            isAboutToFever = true;
            SoundManager.PlaySFX(AudioNameTag.SFX_FEVER_TRANSITION);
        }

        if (curTime >= noteTimeInfo.FeverStartTime)
        {
            isFever = true;
            Debug.Log("FEVER!");

            GameController.Instance.PostProcessControl.PlayFeverEffect();
            UIManager.Instance.uiDirector.ActivateFever();
            GameController.Instance.FireEffectControl.EnableFireEffect();

            float playBackSpeed = GameController.Instance.FeverPlaybackSpeed;
            SoundManager.SetBgmSpeed(playBackSpeed);

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
                feverNotes[i].SetActive(true);
            }
        }
    }

    void CheckGameEnd()
    {
        if(curTime >= noteTimeInfo.PlayTime)
        {
            Debug.Log("게임 종료");
            isPlay = false;

            GameController.Instance.StopPlayerAnimation();
            GameController.Instance.PostProcessControl.StopFeverEffect();
            GameController.Instance.FireEffectControl.DisableFireEffect();

            SoundManager.SetBgmSpeed();

            for (int i = 0; i < notes.Count; i++)
            {
                notes[i].GetComponent<Note>().SetFever(false);
            }
            for (int i = 0; i < notes.Count; i++)
            {
                feverNotes[i].SetActive(false);
                inCircleNotes[i].SetActive(false);
            }
        }
    }

}
