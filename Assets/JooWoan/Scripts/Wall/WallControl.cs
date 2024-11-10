using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
    1. Wall         :   Actual walls within the inspector, total: 4
    2. Block        :   Positions where walls can be placed at.

    3. playerBlocks :   Blocks where players are positioned.
                        Each player takes up 2 blocks.
                        
                        ex)
                        playerBlocks = [P1, P1, P2, P2]

    4. wallBlocks   :   Blocks where walls are positioned.
*/
public class WallControl : MonoBehaviour
{
    [SerializeField] private List<Transform> playerTransforms;
    [SerializeField] private List<int> playerBlocks = new List<int>();
    [SerializeField] private List<int> wallBlocks = new List<int>();
    private List<Transform> walls = new List<Transform>();
    private List<TreeMixup> bgTrees = new List<TreeMixup>();
    private int[] blocks = new int[2];
    private float wallHeight;
    private float firstWallRectBottom, firstWallRectCenter;

    void Start()
    {
        foreach (Transform wall in transform)
        {
            walls.Add(wall);
            wallBlocks.Add(0);
            playerBlocks.Add(0);

            TreeMixup bgTree = wall.GetComponent<TreeMixup>();

            if (bgTree)
                bgTrees.Add(bgTree);
        }
        wallHeight = walls[0].GetComponent<MeshRenderer>().bounds.size.y;
        firstWallRectBottom = walls[0].transform.position.y - wallHeight / 2;
        firstWallRectCenter = walls[0].transform.position.y;

        SetupWalls();
    }

    void Update()
    {
        EnableWalls();
    }

    private void SetupWalls()
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

            blocks[0] = (int)Mathf.Round(playerPosY);
            blocks[1] = blocks[0] - 1;
            
            UpdatePlayerBlock(i * 2, blocks[0]);
            UpdatePlayerBlock(i * 2 + 1, blocks[1]);

            if (!IsWallPlacedAt(blocks[0]))
            {
                int newBlock = blocks[0];
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
        walls[wallIndex].position = new Vector3(
            walls[wallIndex].position.x,
            firstWallRectCenter + blockIndex * wallHeight,
            walls[wallIndex].position.z
        );
        wallBlocks[wallIndex] = blockIndex;
        bgTrees[wallIndex].ChangeTreeObject();
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
