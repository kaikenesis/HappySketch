using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class TreeMixup : MonoBehaviour
{
    private List<GameObject> treeChildBlocks = new List<GameObject>();
    private List<List<Transform>> treeTransforms = new List<List<Transform>>();
    private int currentTreeBlockIndex;
    void Start()
    {
        foreach (Transform treeTransform in transform)
        {
            treeChildBlocks.Add(treeTransform.gameObject);
            treeTransforms.Add(new List<Transform>());

            foreach (Transform tree in treeTransform)
            {
                treeTransforms[treeTransforms.Count - 1].Add(tree);
                tree.gameObject.SetActive(false);
            }
        }
        if (treeChildBlocks.Count == 0)
            return;

        currentTreeBlockIndex = 0;
        EnableTrees(0);
    }

    public void ResetTreeBlock()
    {
        for (int i = 0; i < treeChildBlocks.Count; i++)
            DisableTrees(i);

        currentTreeBlockIndex = 0;
        EnableTrees(0);
    }

    public void ChangeTreeObject(int currentBlockIndex)
    {
        if (treeChildBlocks.Count == 0)
            return;

        int randomIndex = Random.Range(0, treeChildBlocks.Count);

        DisableTrees(currentTreeBlockIndex);
        EnableTrees(randomIndex, currentBlockIndex);

        currentTreeBlockIndex = randomIndex;
    }

    private void EnableTrees(int index, int currentBlockIndex = 0)
    {
        int totalTreeCount = treeTransforms[index].Count;

        if (currentBlockIndex >= GameController.Instance.DecreaseTreeBlockIndex)
        {
            totalTreeCount -= (currentBlockIndex - GameController.Instance.DecreaseTreeBlockIndex) / GameController.Instance.BlocksPerTreeDecrease;

            if (totalTreeCount <= 0)
                totalTreeCount = 0;
        }
        HashSet<int> treeIndexes = new HashSet<int>();

        while (treeIndexes.Count < totalTreeCount)
        {
            int randomIndex = Random.Range(0, totalTreeCount);
            treeIndexes.Add(randomIndex);
        }
        foreach (int randIdx in treeIndexes)
            treeTransforms[index][randIdx].gameObject.SetActive(true);

        treeChildBlocks[index].SetActive(true);
    }

    private void DisableTrees(int index)
    {
        if (index < 0)
            return;

        treeChildBlocks[index].SetActive(false);

        foreach (Transform treeTransform in treeChildBlocks[index].transform)
            treeTransform.gameObject.SetActive(false);
    }
}
