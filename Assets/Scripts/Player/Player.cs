using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private KeyCode leftHand, rightHand, leftFoot, rightFoot;
    private float currentSpeed;
    private bool isMoving = false;

    void Start()
    {
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        if (PressedBtn(leftHand))
            isMoving = true;
        
        MoveUp();
    }

    void MoveUp()
    {
        if (!isMoving)
            return;
        
        transform.Translate(0, currentSpeed / 50.0f, 0);
        currentSpeed *= 0.98f;

        if (currentSpeed <= 0.1f)
        {
            isMoving = false;
            currentSpeed = moveSpeed;
        }
    }

    public bool PressedBtn(KeyCode keycode)
    {
        return Input.GetKeyDown(keycode);
    }
}
