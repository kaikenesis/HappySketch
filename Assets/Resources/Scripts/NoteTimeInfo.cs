using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoteTimeInfo", menuName = "ScriptableObjects/NoteTimeInfo", order = 1)]
public class NoteTimeInfo : ScriptableObject
{
    [SerializeField] float[] totalTime;
    [SerializeField] float[] perfectTime;
    [SerializeField] float[] goodTime;

    [SerializeField] int perfectScore;
    [SerializeField] int goodScore;
    [SerializeField] int badScore;

}
