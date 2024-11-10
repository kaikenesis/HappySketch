using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMixup : MonoBehaviour
{
    private List<GameObject> treeObjects = new List<GameObject>();
    private int currentTreeObjectIdx;

    void Start()
    {
        foreach (Transform treeTransform in transform)
            treeObjects.Add(treeTransform.gameObject);

        if (treeObjects.Count == 0)
            return;
        
        treeObjects[0].SetActive(true);
        currentTreeObjectIdx = 0;
    }

    public void ChangeTreeObject()
    {
        if (treeObjects.Count == 0)
            return;
        
        int randomIndex = Random.Range(0, treeObjects.Count);

        treeObjects[currentTreeObjectIdx].SetActive(false);
        treeObjects[randomIndex].SetActive(true);

        currentTreeObjectIdx = randomIndex;
    }
}
