using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    public static GameController Instance => instance;

    private IDictionary<int, Player> playerDict = new Dictionary<int, Player>();
    public IDictionary<int, Player> PlayerDict => playerDict;

    [SerializeField] private float heightPerIncrease;
    public float HeightPerIncrease => heightPerIncrease;

    private GameObject firstFloor;

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
            if (playerInfo.Value.CurrentHeight <= 2500f)
            {
                canDisable = false;
                break;
            }
        }
        if (canDisable)
            firstFloor.SetActive(false);
    }
}