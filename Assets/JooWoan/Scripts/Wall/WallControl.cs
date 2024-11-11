using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
    1. Block        :   Positions where walls can be placed at.

    2. playerBlocks :   Blocks where players are positioned.
                        Each player takes up 2 blocks.
                        
                        ex)
                        playerBlocks = [P1, P1, P2, P2]

    3. wallBlocks   :   Blocks where walls are positioned.
*/
public class WallControl : MonoBehaviour
{
    [SerializeField] private List<Transform> playerTransforms;
    [SerializeField] private List<Transform> blocks = new List<Transform>();
    [SerializeField] private List<TreeMixup> treeBlocks = new List<TreeMixup>();
    [SerializeField] private List<GameObject> cloudBlocks = new List<GameObject>();
    [SerializeField] private MeshRenderer wallRenderer;

    [SerializeField] private List<int> playerBlocks = new List<int>();
    [SerializeField] private List<int> wallBlocks = new List<int>();
    private int[] tempBlocks = new int[2];
    
    private float wallHeight;
    private float firstWallRectBottom, firstWallRectCenter;

    void Start()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            wallBlocks.Add(0);
            playerBlocks.Add(0);
        }
        wallHeight = wallRenderer.bounds.size.y;
        firstWallRectBottom = blocks[0].transform.position.y - wallHeight / 2;
        firstWallRectCenter = blocks[0].transform.position.y;

        SetupWalls();
    }

    void Update()
    {
        EnableWalls();
    }

    public void ResetBlockStates()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            wallBlocks[i] = 0;
            playerBlocks[i] = 0;
        }
        SetupWalls();
        for (int i = 0; i < treeBlocks.Count; i++)
            treeBlocks[i].ResetTreeBlocks();
    }

    public void SetupWalls()
    {
        MoveWall(0, 0);
        MoveWall(1, -1);
        MoveWall(2, -2);
        MoveWall(3, -3);
    }

    private void EnableWalls()
    {
        for (int i = 0; i < playerTransforms.Count; i++)
        {
            float playerPosY = (playerTransforms[i].position.y - firstWallRectBottom) / wallHeight;

            tempBlocks[0] = (int)Mathf.Round(playerPosY);
            tempBlocks[1] = tempBlocks[0] - 1;
            
            UpdatePlayerBlock(i * 2, tempBlocks[0]);
            UpdatePlayerBlock(i * 2 + 1, tempBlocks[1]);

            if (!IsWallPlacedAt(tempBlocks[0]))
            {
                int newBlock = tempBlocks[0];
                AdjustWalls(newBlock);
            }
        }
    }

    private void AdjustWalls(int newBlock)
    {
        for (int j = 0; j < wallBlocks.Count; j++)
        {
            bool canMove = true;
            for (int k = 0; k < playerBlocks.Count; k++)
            {
                if (wallBlocks[j] == playerBlocks[k])
                {
                    canMove = false;
                    break;
                }
            }
            if (canMove)
            {
                MoveWall(j, newBlock);
                return;
            }
        }
    }

    private void MoveWall(int wallIndex, int blockIndex)
    {
        blocks[wallIndex].position = new Vector3(
            blocks[wallIndex].position.x,
            firstWallRectCenter + blockIndex * wallHeight,
            blocks[wallIndex].position.z
        );
        wallBlocks[wallIndex] = blockIndex;
        treeBlocks[wallIndex].ChangeTreeObject();

        bool enableCloud = blockIndex >= GameController.Instance.EnableCloudBlockIndex;
        cloudBlocks[wallIndex].SetActive(enableCloud);
    }

    private void UpdatePlayerBlock(int wallIndex, int blockIndex)
    {
        playerBlocks[wallIndex] = blockIndex;
    }

    private bool IsWallPlacedAt(int blockIndex)
    {
        for (int i = 0; i < wallBlocks.Count; i++)
        {
            if (wallBlocks[i] == blockIndex)
                return true;
        }
        return false;
    }
}
