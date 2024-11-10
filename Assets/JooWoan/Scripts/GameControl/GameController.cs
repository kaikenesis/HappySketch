using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("_GameController");
                instance = newGameObject.AddComponent<GameController>();
            }
            return instance;
        }
    }

    private IDictionary<PlayerNumber, Player> playerDict = new Dictionary<PlayerNumber, Player>();
    public IDictionary<PlayerNumber, Player> PlayerDict => playerDict;

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

    public void RegisterPlayer(PlayerNumber playerNumber, Player newPlayer)
    {
        if (playerDict.TryGetValue(playerNumber, out Player _))
            return;

        playerDict.Add(playerNumber, newPlayer);
    }

    public void MoveupPlayer(PlayerNumber playerNumber, int repeat)
    {
        StartCoroutine(playerDict[playerNumber].MoveUp(repeat));
    }
}

public enum PlayerNumber
{
    PLAYER_1,
    PLAYER_2
}