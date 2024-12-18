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
public class BlockControl : MonoBehaviour
{
    public List<int> PlayerBlockIndexes => playerBlockIndexes;

    [SerializeField] private List<Transform> playerTransforms;
    [SerializeField] private List<Transform> blocks = new List<Transform>();
    [SerializeField] private List<TreeMixup> treeBlocks = new List<TreeMixup>();
    [SerializeField] private List<BirdSpawner> birdBlocks = new List<BirdSpawner>();
    [SerializeField] private List<GameObject> cloudBlocks = new List<GameObject>();
    [SerializeField] private MeshRenderer wallRenderer;

    [SerializeField] private List<int> playerBlockIndexes = new List<int>();
    [SerializeField] private List<int> actualBlockIndexes = new List<int>();
    private int[] newBlocks = new int[2];
    
    private float blockHeight;
    private float firstBlockRectBottom, firstBlockRectCenter;

    void Start()
    {
        GameController.Instance.InitWallControl(this);

        for (int i = 0; i < blocks.Count; i++)
        {
            actualBlockIndexes.Add(0);
            playerBlockIndexes.Add(0);
        }
        blockHeight = wallRenderer.bounds.size.y;
        firstBlockRectBottom = blocks[0].transform.position.y - blockHeight / 2;
        firstBlockRectCenter = blocks[0].transform.position.y;

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
            actualBlockIndexes[i] = 0;
            playerBlockIndexes[i] = 0;
        }
        SetupWalls();
        for (int i = 0; i < treeBlocks.Count; i++)
            treeBlocks[i].ResetTreeBlock();
    }

    public void SetupWalls()
    {
        MoveBlock(0,  0);
        MoveBlock(1, -1);
        MoveBlock(2, -2);
        MoveBlock(3, -3);
    }

    private void EnableWalls()
    {
        for (int i = 0; i < playerTransforms.Count; i++)
        {
            float playerPosY = (playerTransforms[i].position.y - firstBlockRectBottom) / blockHeight;

            newBlocks[0] = (int)Mathf.Round(playerPosY);
            newBlocks[1] = newBlocks[0] - 1;
            
            UpdatePlayerBlock(i * 2, newBlocks[0]);
            UpdatePlayerBlock(i * 2 + 1, newBlocks[1]);

            if (!IsBlockPlacedAt(newBlocks[0]))
                AdjustBlocks(newBlocks[0]);
        }
    }

    private void AdjustBlocks(int blockIndex)
    {
        for (int j = 0; j < actualBlockIndexes.Count; j++)
        {
            bool canMove = true;
            for (int k = 0; k < playerBlockIndexes.Count; k++)
            {
                if (actualBlockIndexes[j] == playerBlockIndexes[k])
                {
                    canMove = false;
                    break;
                }
            }
            if (canMove)
            {
                MoveBlock(j, blockIndex);
                return;
            }
        }
    }

    private void MoveBlock(int listIndex, int blockIndex)
    {
        blocks[listIndex].position = new Vector3(
            blocks[listIndex].position.x,
            firstBlockRectCenter + blockIndex * blockHeight,
            blocks[listIndex].position.z
        );
        actualBlockIndexes[listIndex] = blockIndex;
        treeBlocks[listIndex].ChangeTreeObject(blockIndex);

        bool enableCloud = blockIndex >= GameController.Instance.EnableCloudBlockIndex;
        cloudBlocks[listIndex].SetActive(enableCloud);

        birdBlocks[listIndex].TrySpawnBirds(blockIndex);
    }

    private void UpdatePlayerBlock(int listIndex, int blockIndex)
    {
        playerBlockIndexes[listIndex] = blockIndex;
    }

    private bool IsBlockPlacedAt(int blockIndex)
    {
        for (int i = 0; i < actualBlockIndexes.Count; i++)
        {
            if (actualBlockIndexes[i] == blockIndex)
                return true;
        }
        return false;
    }
}
