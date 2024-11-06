using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private List<GameObject> notes = new List<GameObject>();
    [SerializeField] NoteTimeInfo noteTimeInfo;
    private int level = 0;
    private bool canEnable = true;

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
        if(canEnable)
        {
            StartCoroutine(EnableNote());
        }
        CheckNotes();
    }    

    void SetLevel(int lv)
    {
        for(int i = 0; i < notes.Count; i++)
        {
            notes[i].GetComponent<Note>().level = lv;
        }
    }

    IEnumerator EnableNote()
    {
        canEnable = false;
        Debug.Log("생성");
        for (int i = 0;i < notes.Count; i++)
        {
            notes[i].SetActive(true);
        }
        yield return new WaitForSeconds(noteTimeInfo.TotalTime[level]);
        Debug.Log("턴 종료");
        for (int i = 0; i < notes.Count; i++)
        {
            notes[i].SetActive(false);
        }
        StartCoroutine(WaitRecreate());
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
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (notes[0].activeSelf ==  true)
                notes[0].GetComponent<Note>().Check();
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (notes[1].activeSelf == true)
                notes[1].GetComponent<Note>().Check();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (notes[2].activeSelf == true)
                notes[2].GetComponent<Note>().Check();
            return;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (notes[3].activeSelf == true)
                notes[3].GetComponent<Note>().Check();
            return;
        }        
    }
}
