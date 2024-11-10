using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    [SerializeField] private KeyCode leftHand, rightHand, leftFoot, rightFoot;
    [SerializeField] private float animationSpeed;
    [SerializeField] private List<AnimationClip> climbAnimClips;

    private Animator playerAnim;
    private int animIndex = 0;
    private bool isPlayingAnimation = false;

    [SerializeField] private float currentHeight = 0;
    public float CurrentHeight => currentHeight;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAnim.speed = animationSpeed;
        GameController.Instance.RegisterPlayer(playerNumber, this);
    }

    void Update()
    {
        if (PressedBtn(leftHand))
            StartCoroutine(MoveUp(1));
    }

    public IEnumerator MoveUp(int repeat)
    {
        if (isPlayingAnimation)
            yield break;
        
        currentHeight += GameController.Instance.HeightPerIncrease * repeat;

        isPlayingAnimation = true;
        for (int i = 0; i < repeat; i++)
        {
            float animDuration = climbAnimClips[animIndex].length / playerAnim.speed;

            playerAnim.Play(climbAnimClips[animIndex].name, -1, 0f);
            animIndex = (animIndex + 1) % climbAnimClips.Count;
            
            yield return new WaitForSeconds(animDuration);
        }
        isPlayingAnimation = false;
    }

    public bool PressedBtn(KeyCode keycode)
    {
        return Input.GetKeyDown(keycode);
    }
}
