using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    public float HeightPerIncrease => heightPerIncrease;
    public int EnableCloudBlockIndex => enableCloudBlockIndex;

    [SerializeField] private float heightPerIncrease;
    [SerializeField] private float disableFirstFloorHeight;
    [SerializeField] private int enableCloudBlockIndex;

    private IDictionary<int, Player> playerDict = new Dictionary<int, Player>();
    private GameObject firstFloor;

    void Start()
    {
        firstFloor = GameObject.FindGameObjectWithTag("GameFirstFloor");
    }

    public void RegisterPlayer(int playerNumber, Player newPlayer)
    {
        if (playerDict.TryGetValue(playerNumber, out Player _))
            return;

        playerDict.Add(playerNumber, newPlayer);
    }

    public void MoveupPlayer(int playerNumber, int repeat)
    {
        StartCoroutine(playerDict[playerNumber].MoveUp(repeat));
    }

    public void TryDisableFirstFloor()
    {
        bool canDisable = true;
        foreach (KeyValuePair<int, Player> playerInfo in playerDict)
        {
            if (playerInfo.Value.CurrentHeight <= disableFirstFloorHeight)
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
}