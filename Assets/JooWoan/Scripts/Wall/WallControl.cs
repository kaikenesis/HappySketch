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
    private float wallHeight;
    private float firstWallRectBottom, firstWallRectCenter;
    private int[] blocks = new int[2];

    void Start()
    {
        foreach (Transform wall in transform)
        {
            walls.Add(wall);
            currentBlockIndexes.Add(0);
        }
        wallHeight = walls[0].GetComponent<MeshRenderer>().bounds.size.y;
        firstWallRectBottom = walls[0].transform.position.y - wallHeight / 2;
        firstWallRectCenter = walls[0].transform.position.y;
    }
    void Update()
    {
        EnableWalls();
    }

    void EnableWalls()
    {
        for (int i = 0; i < playerTransforms.Count; i++)
        {
            float playerPosY = (playerTransforms[i].position.y - firstWallRectBottom) / wallHeight;

            blocks[0] = (int)Mathf.Round(playerPosY);
            blocks[1] = Mathf.Max(0, blocks[0] - 1);

            for (int j = 0; j < blocks.Length; j++)
            {
                if (!IsExistingWall(blocks[j]))
                {
                    currentBlockIndexes[i * 2 + j]  = blocks[j];

                    walls[i * 2 + j].position = new Vector3(
                        walls[i * 2 + j].position.x,
                        firstWallRectCenter + blocks[j] * wallHeight,
                        walls[i * 2 + j].position.z
                    );
                }
            }
        }
    }

    bool IsExistingWall(int upperBlockIndex)
    {
        for (int i = 0; i < currentBlockIndexes.Count; i++)
        {
            if (upperBlockIndex == currentBlockIndexes[i])
                return true;
        }
        return false;
    }
}
