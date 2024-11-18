using System.Collections.Generic;
using UnityEngine;
using HappySketch;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    public static GameController Instance => instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public IDictionary<int, Player> PlayerDict => playerDict;
    public int EnableCloudBlockIndex => enableCloudBlockIndex;
    public int DecreaseTreeBlockIndex => decreaseTreeBlockIndex;
    public int BlocksPerTreeDecrease => blocksPerTreeDecrease;

    [SerializeField] private float disableFirstFloorHeight;
    [SerializeField] private int enableCloudBlockIndex;
    [SerializeField] private int decreaseTreeBlockIndex;
    [SerializeField] private int blocksPerTreeDecrease;

    private IDictionary<int, Player> playerDict = new Dictionary<int, Player>();
    private GameObject firstFloor, bgRealDome, bgToonDome;

    void Start()
    {
        firstFloor = GameObject.FindGameObjectWithTag("GameFirstFloor");
        bgRealDome = GameObject.FindGameObjectWithTag("BgDomeRealistic");
        bgToonDome = GameObject.FindGameObjectWithTag("BgDomeToon");
        bgRealDome.SetActive(false);
    }

    public void RegisterPlayer(int playerNumber, Player newPlayer)
    {
        if (playerDict.TryGetValue(playerNumber, out Player _))
            return;

        playerDict.Add(playerNumber, newPlayer);
    }

    public void MoveupPlayer(int playerNumber)
    {
        playerDict[playerNumber].QueueAnimationRepeat();
    }

    public void StopPlayerAnimation()
    {
        foreach (KeyValuePair<int, Player> playerInfo in playerDict)
            playerInfo.Value.ClearAnimationRepeat();
    }

    // Check each player's height(score), if both are high enough disable first floor
    public void TryDisableFirstFloor(int[] playerScores)
    {
        bool canDisable = true;

        for (int i = 0; i < playerScores.Length; i++)
        {
            if (playerScores[i] < disableFirstFloorHeight)
            {
                canDisable = false;
                break;
            }
        }
        if (canDisable)
            firstFloor.SetActive(false);
    }

    public Camera GetPlayerCamera(int playerNumber)
    {
        if (playerDict.TryGetValue(playerNumber, out Player player))
            return player.PlayerCam;

        Debug.LogWarning($"Player {playerNumber} does not exist");
        return null;
    }

    public void SetBackgroundDome(BgDomeType type, bool state)
    {
        switch (type)
        {
            case BgDomeType.REALISTIC:
                bgRealDome.SetActive(state);
                break;

            case BgDomeType.TOON:
                bgToonDome.SetActive(state);
                break;

            default:
                return;
        }
    }
}

public enum BgDomeType
{
    REALISTIC,
    TOON
}