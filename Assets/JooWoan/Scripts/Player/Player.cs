using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private KeyCode leftHand, rightHand, leftFoot, rightFoot;
    [SerializeField] private float animationSpeed;
    [SerializeField] private List<AnimationClip> climbAnimClips;
    private Animator playerAnim;
    private int animIndex = 0;
    private bool isPlayingAnimation = false;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAnim.speed = animationSpeed;
    }

    void Update()
    {
        if (PressedBtn(leftHand) && !isPlayingAnimation)
            MoveUp();
    }

    void MoveUp()
    {
        float animDuration = climbAnimClips[animIndex].length / playerAnim.speed;
        StartCoroutine(ToggleIsPlayingAnimation(animDuration));

        playerAnim.Play(climbAnimClips[animIndex].name, -1, 0f);
        animIndex = (animIndex + 1) % climbAnimClips.Count;
    }


    IEnumerator ToggleIsPlayingAnimation(float duration)
    {
        isPlayingAnimation = true;
        yield return new WaitForSeconds(duration);
        isPlayingAnimation = false;
    }

    public bool PressedBtn(KeyCode keycode)
    {
        return Input.GetKeyDown(keycode);
    }
}
