using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    [SerializeField] private float animationSpeed;
    [SerializeField] private List<AnimationClip> climbAnimClips;
    [SerializeField] private Camera playerCam;
    public Camera PlayerCam => playerCam;

    private Animator playerAnim;
    private int animIndex = 0;
    private bool isPlayingAnimation = false;

    int animationRepeat = 0;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAnim.speed = animationSpeed;
        GameController.Instance.RegisterPlayer(playerNumber, this);
    }

    void Update()
    {
        MoveUp();
    }

    public void QueueAnimationRepeat()
    {
        animationRepeat++;
    }

    public void ClearAnimationRepeat()
    {
        animationRepeat = 0;
    }

    private void MoveUp()
    {
        if (isPlayingAnimation || animationRepeat <= 0)
            return;

        animationRepeat--;
        isPlayingAnimation = true;

        float animDuration = climbAnimClips[animIndex].length / playerAnim.speed;
        Invoke("DisableIsPlayingAnimation", animDuration);

        playerAnim.Play(climbAnimClips[animIndex].name, -1, 0f);
        animIndex = (animIndex + 1) % climbAnimClips.Count;
    }

    private void DisableIsPlayingAnimation()
    {
        isPlayingAnimation = false;
    }
}
