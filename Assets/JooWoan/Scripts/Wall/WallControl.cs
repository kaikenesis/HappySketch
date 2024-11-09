using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

/*
    1. Wall  : Actual walls within the inspector, total: 4
    2. Block : Positions where walls can be placed at.
*/
public class WallControl : MonoBehaviour
{
    [SerializeField] private List<Transform> playerTransforms;
    [SerializeField] private List<int> currentBlockIndexes = new List<int>();
    private List<Transform> walls = new List<Transform>();
    private List<TreeMixup> bgTrees = new List<TreeMixup>();
    private float wallHeight;
    private float firstWallRectBottom, firstWallRectCenter;
    private int[] blocks = new int[2];

    void Start()
    {
        foreach (Transform wall in transform)
        {
            walls.Add(wall);
            currentBlockIndexes.Add(999);

            TreeMixup bgTree = wall.GetComponent<TreeMixup>();

            if (bgTree)
                bgTrees.Add(bgTree);
        }
        wallHeight = walls[0].GetComponent<MeshRenderer>().bounds.size.y;
        firstWallRectBottom = walls[0].transform.position.y - wallHeight / 2;
        firstWallRectCenter = walls[0].transform.position.y;
    }

    void Update()
    {
        EnableWalls();
    }

    private void EnableWalls()
    {
        for (int i = 0; i < playerTransforms.Count; i++)
        {
            float playerPosY = (playerTransforms[i].position.y - firstWallRectBottom) / wallHeight;

            blocks[0] = (int)Mathf.Round(playerPosY);
            blocks[1] = blocks[0] - 1;

            bool flag = false;

            for (int j = 0; j < 2; j++)
            {
                if (!IsExistingWall(blocks[j]))
                {
                    flag = true;
                    UpdateWallIndex(i * 2 + j, blocks[j]);
                }
            }
            if (flag)
                UpdateWallPositions(i * 2, blocks[0]);
        }
    }

    private bool IsExistingWall(int blockIndex)
    {
        for (int i = 0; i < currentBlockIndexes.Count; i++)
        {
            if (blockIndex == currentBlockIndexes[i])
                return true;
        }
        return false;
    }

    private void UpdateWallPositions(int currentWallIndex, int newBlockIndex)
    {
        for (int i = 0; i < 2; i++)
        {
            int blockIndex = (int)(
                (walls[currentWallIndex + i].position.y - firstWallRectCenter) / wallHeight
            );
            if (!IsExistingWall(blockIndex))
            {
                MoveWall(currentWallIndex + i, newBlockIndex);
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
        bgTrees[wallIndex].ChangeTreeObject();
    }

    private void UpdateWallIndex(int wallIndex, int blockIndex)
    {
        currentBlockIndexes[wallIndex] = blockIndex;
    }
}
