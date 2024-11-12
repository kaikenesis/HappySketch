using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class TreeMixup : MonoBehaviour
{
    private List<GameObject> treeChildBlocks = new List<GameObject>();
    private int currentBlockIndex;

    void Start()
    {
        foreach (Transform treeTransform in transform)
            treeChildBlocks.Add(treeTransform.gameObject);

        if (treeChildBlocks.Count == 0)
            return;

        treeChildBlocks[0].SetActive(true);
        currentBlockIndex = 0;
    }

    public void ResetTreeBlock()
    {
        for (int i = 0; i < treeChildBlocks.Count; i++)
        {
            treeChildBlocks[i].SetActive(false);

            foreach (Transform tree in treeChildBlocks[i].transform)
                tree.gameObject.SetActive(true);
        }
        treeChildBlocks[0].SetActive(true);
        currentBlockIndex = 0;
    }

    public void ChangeTreeObject()
    {
        if (treeChildBlocks.Count == 0)
            return;
        
        int randomIndex = Random.Range(0, treeChildBlocks.Count);

        treeChildBlocks[currentBlockIndex].SetActive(false);
        treeChildBlocks[randomIndex].SetActive(true);

        currentBlockIndex = randomIndex;
    }

    private void EnableTrees(int index)
    {
        int totalTreeCount = 0;

        foreach (Transform treeTransform in treeChildBlocks[index].transform)
            totalTreeCount++;

        HashSet<int> treeIndexes = new HashSet<int>();
        int randomTreeCount = Random.Range(0, totalTreeCount + 1);

        while (treeIndexes.Count < randomTreeCount)
        {
            int randomIndex = Random.Range(0, totalTreeCount);
            treeIndexes.Add(randomIndex);
        }
        //foreach (int idx in treeChildBlocks[index].transform)
    }

    private void DisableTrees(int index)
    {
        treeChildBlocks[index].SetActive(false);
        foreach (Transform treeTransform in treeChildBlocks[index].transform)
            treeTransform.gameObject.SetActive(true);
    }
}
