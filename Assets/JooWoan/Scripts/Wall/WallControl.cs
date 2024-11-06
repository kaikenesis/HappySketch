using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class WallControl : MonoBehaviour
{
    [SerializeField] private List<Transform> playerTransforms;
    private List<int> activatedTileIndexes = new List<int>();

    private List<Transform> walls = new List<Transform>();
    private float wallHeight;
    private float startY;

    void Start()
    {
        foreach (Transform wall in transform)
        {
            walls.Add(wall);
            activatedTileIndexes.Add(-1);
        }
        wallHeight = walls[0].GetComponent<MeshRenderer>().bounds.size.y;
        startY = walls[0].transform.position.y - wallHeight / 2;
    }
    void Update()
    {
        EnableWalls();
    }

    void EnableWalls()
    {
        for (int i = 0; i < playerTransforms.Count; i++)
        {
            float playerPosY = (playerTransforms[i].position.y - startY) / wallHeight;
            int lowerTile = (int)Mathf.Floor(playerPosY);
            int upperTile = (int)Mathf.Ceil(playerPosY);

            Debug.Log($"{lowerTile} , {upperTile}");

            if (activatedTileIndexes[i * 2] == lowerTile)
                continue;

            activatedTileIndexes[i * 2] = lowerTile;
            activatedTileIndexes[i * 2 + 1] = upperTile;

            walls[i * 2].position = new Vector3(
                walls[i * 2].position.x,
                walls[0].position.y + lowerTile * wallHeight,
                walls[i * 2].position.z
            );
            walls[i * 2 + 1].position = new Vector3(
                walls[i * 2 + 1].position.x,
                walls[0].position.y + upperTile * wallHeight,
                walls[i * 2 + 1].position.z
            );
        }

    }
}
