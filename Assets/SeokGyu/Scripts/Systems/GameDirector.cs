using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.curLevel = ELevel.MAX;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
