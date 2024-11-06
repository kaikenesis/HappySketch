using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoteTimeInfo", menuName = "ScriptableObjects/NoteTimeInfo", order = 1)]
public class NoteTimeInfo : ScriptableObject
{
    [SerializeField] float[] totalTime;
    [SerializeField] float[] perfectTime;
    [SerializeField] float[] goodTime;
    [SerializeField] float minRecreateTime;
    [SerializeField] float maxRecreateTime;
    [SerializeField] float feverStartTime;
    [SerializeField] float feverCheckTime;
    [SerializeField] int perfectScore;
    [SerializeField] int goodScore;
    [SerializeField] int badScore;
    [SerializeField] float playTime;


    public float[] TotalTime { get { return totalTime; } }
    public float[] PerfectTime { get { return perfectTime; } }
    public float[] GoodTime { get { return goodTime; } }
    public float MinRecreateTime { get { return minRecreateTime; } }
    public float MaxRecreateTime { get { return maxRecreateTime; } }
    public float FeverStartTime {  get { return feverStartTime; } }
    public float FeverCheckTime { get { return feverCheckTime; } }
    public float PlayTime { get { return playTime; } }
    public int PerfectScore { get { return perfectScore; } }
    public int GoodScore { get { return goodScore; } }
    public int BadScore { get { return badScore; } }
}
