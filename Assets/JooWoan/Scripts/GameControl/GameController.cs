using System.Collections.Generic;
using UnityEngine;
using Player = HappySketch.Player;

public class GameController : MonoBehaviour
{
    #region Singleton
    private static GameController instance = null;
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
    #endregion
    
    #region Public Lambdas
    
    public static GameController Instance => instance;
    public PostProcessingControl PostProcessControl => postProcessingControl;
    public IDictionary<int, Player> PlayerDict => playerDict;
    public int EnableCloudBlockIndex => enableCloudBlockIndex;
    public int EnableBirdBlockIndex => enableBirdBlockIndex;
    public int DecreaseTreeBlockIndex => decreaseTreeBlockIndex;
    public int BlocksPerTreeDecrease => blocksPerTreeDecrease;
    public float BirdSpawnProbability => birdSpawnProbability;
    #endregion

    private PostProcessingControl postProcessingControl;
    private WallControl wallControl;

    [SerializeField] private float disableFirstFloorHeight;
    [SerializeField] private int enableCloudBlockIndex;
    [SerializeField] private int enableBirdBlockIndex;
    [SerializeField] private int decreaseTreeBlockIndex;

    [Range(0.0f, 100.0f)]
    [SerializeField] private float birdSpawnProbability;

    private IDictionary<int, Player> playerDict = new Dictionary<int, Player>();
    private GameObject firstFloor, bgRealDome, bgToonDome;
    private int blocksPerTreeDecrease = 1;

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

    public void InitWallControl(WallControl wallControl)
    {
        this.wallControl = wallControl;
    }

    public void InitPostprocessing(PostProcessingControl postProcessingControl)
    {
        this.postProcessingControl = postProcessingControl;
    }

    public void ResetLevel()
    {
        foreach (Player player in playerDict.Values)
        {
            player.ResetAnimation();
            player.ResetTransform();
            player.CameraControl.ResetPosition();
        }
        wallControl.ResetBlockStates();
    }
}

public enum BgDomeType
{
    REALISTIC,
    TOON
}