using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private List<GameObject> notes;
    
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject obj = new GameObject();
            obj.name = "note" + (i + 1);
            obj.AddComponent<Note>();            
            notes.Add(obj);
        }
    }

    void Update()
    {
        
    }

    void SetLevel(int lv)
    {
        for(int i = 0; i < notes.Count; i++)
        {
            notes[i].GetComponent<Note>().level = lv;
        }
    }

    void CheckNotes()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            notes[0].GetComponent<Note>().Check();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            notes[1].GetComponent<Note>().Check();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            notes[2].GetComponent<Note>().Check();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            notes[3].GetComponent<Note>().Check();
        }
    }
}
