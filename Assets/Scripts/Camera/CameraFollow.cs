using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.3f;
    private float initialHeight;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        initialHeight = transform.position.y;
    }

    void Update()
    {
        Vector3 targetPos = new Vector3(transform.position.x, (int)target.position.y + 10f, transform.position.z);

        if (targetPos.y < initialHeight)
            return;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
