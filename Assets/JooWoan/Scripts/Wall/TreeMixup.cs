using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMixup : MonoBehaviour
{
    private List<GameObject> treeBlocks = new List<GameObject>();
    private int currentTreeObjectIdx;

    void Start()
    {
        foreach (Transform treeTransform in transform)
            treeBlocks.Add(treeTransform.gameObject);

        if (treeBlocks.Count == 0)
            return;

        treeBlocks[0].SetActive(true);
        currentTreeObjectIdx = 0;
    }

    public void ChangeTreeObject()
    {
        if (treeBlocks.Count == 0)
            return;
        
        int randomIndex = Random.Range(0, treeBlocks.Count);

        treeBlocks[currentTreeObjectIdx].SetActive(false);
        treeBlocks[randomIndex].SetActive(true);

        currentTreeObjectIdx = randomIndex;
    }

    private void EnableTrees(int index)
    {
        int totalTreeCount = 0;

        foreach (Transform treeTransform in treeBlocks[index].transform)
            totalTreeCount++;

        HashSet<int> treeIndexes = new HashSet<int>();
        int randomTreeCount = Random.Range(0, totalTreeCount + 1);

        while (treeIndexes.Count < randomTreeCount)
        {
            int randomIndex = Random.Range(0, totalTreeCount);
            treeIndexes.Add(randomIndex);
        }
        //foreach (int idx in treeBlocks[index].transform)
    }

    private void DisableTrees(int index)
    {
        treeBlocks[index].SetActive(false);
        foreach (Transform treeTransform in treeBlocks[index].transform)
            treeTransform.gameObject.SetActive(true);
    }
}
