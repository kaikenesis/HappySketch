using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    [SerializeField] private Transform player1Transform, player2Transform;
    [SerializeField] private List<Transform> walls;
    [SerializeField] private MeshRenderer meshRenderer;
    private float wallHeight;

    //void Start()
    //{
    //    wallHeight = meshRenderer.bounds.size.y;
    //    walls[2].gameObject.SetActive(false);
    //    walls[3].gameObject.SetActive(false);
    //}

    void Update()
    {

    }
}
