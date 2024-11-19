using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [SerializeField] private AnimationClip startingClip;
    [SerializeField] private float minSpeed, maxSpeed;
    private float moveSpeed;
    private Animator birdAnim;
    private Vector3 initialPos;

    void Awake()
    {
        birdAnim = GetComponent<Animator>();
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        initialPos = transform.localPosition;
    }

    void OnEnable()
    {
        birdAnim.Play(startingClip.name, -1, 0f);
    }

    void OnDisable()
    {
        transform.localPosition = initialPos;
    }

    void Update()
    {
        Move();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BirdDisableTrigger")
            gameObject.SetActive(false);
    }

    private void Move()
    {
        transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
    }

}
