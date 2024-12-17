using UnityEngine;

[CreateAssetMenu(fileName = "NoteTimeInfo", menuName = "ScriptableObjects/NoteTimeInfo", order = 1)]
public class NoteTimeInfo : ScriptableObject
{
    [SerializeField] private float[] totalTime;
    [SerializeField] private float[] perfectTime;
    [SerializeField] private float[] goodTime;
    [SerializeField] private float minRecreateTime;
    [SerializeField] private float maxRecreateTime;
    [SerializeField] private float feverStartTime;
    [SerializeField] private float feverCheckTime;
    [SerializeField] private int perfectScore;
    [SerializeField] private int goodScore;
    [SerializeField] private int badScore;
    [SerializeField] private float playTime;


    public float[] TotalTime { get { return totalTime; } }
    public float[] PerfectTime { get { return perfectTime; } }
    public float[] GoodTime { get { return goodTime; } }
    public float MinRecreateTime { get { return minRecreateTime; } }
    public float MaxRecreateTime { get { return maxRecreateTime; } }
    public float FeverStartTime { get { return feverStartTime; } }
    public float FeverCheckTime { get { return feverCheckTime; } }
    public float PlayTime { get { return playTime; } }
    public int PerfectScore { get { return perfectScore; } }
    public int GoodScore { get { return goodScore; } }
    public int BadScore { get { return badScore; } }
}
