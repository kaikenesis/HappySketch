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

    void Start()
    {
        foreach (Transform wall in transform)
        {
            walls.Add(wall);
            currentBlockIndexes.Add(-1);
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

            int upperBlock = (int)Mathf.Round(playerPosY);
            int lowerBlock = Mathf.Max(0, upperBlock - 1);

            if (IsExistingWall(upperBlock))
                continue;

            currentBlockIndexes[i * 2]      = upperBlock;
            currentBlockIndexes[i * 2 + 1]  = lowerBlock;

            walls[i * 2].position = new Vector3(
                walls[i * 2].position.x,
                firstWallRectCenter + lowerBlock * wallHeight,
                walls[i * 2].position.z
            );
            walls[i * 2 + 1].position = new Vector3(
                walls[i * 2 + 1].position.x,
                firstWallRectCenter + upperBlock * wallHeight,
                walls[i * 2 + 1].position.z
            );
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
