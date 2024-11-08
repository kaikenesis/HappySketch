using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMixup : MonoBehaviour
{
    [SerializeField] private List<GameObject> treeObjects;
    private int currentTreeObjectIdx;

    void Start()
    {
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
