using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappySketch
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int playerNumber;
        [SerializeField] private float animationSpeed;
        [SerializeField] private List<AnimationClip> climbAnimClips;
        [SerializeField] private Camera playerCam;
        [SerializeField] private KeyCode moveKey;
        public Camera PlayerCam => playerCam;

        private WaitForEndOfFrame waitEndOfFrame = new WaitForEndOfFrame();
        private Animator playerAnim;

        private int animIndex = 0;
        int animationRepeat = 0;

        private bool isPlayingAnimation = false;

        void Start()
        {
            playerAnim = GetComponent<Animator>();
            playerAnim.speed = animationSpeed;
            GameController.Instance.RegisterPlayer(playerNumber, this);

            StartCoroutine(MoveUp());
        }

        void Update()
        {
            ////////////
            if (Input.GetKeyDown(moveKey))
                QueueAnimationRepeat();
            ////////////
        }

        public void QueueAnimationRepeat()
        {
            animationRepeat++;
        }

        public void ClearAnimationRepeat()
        {
            animationRepeat = 0;
        }

        private IEnumerator MoveUp()
        {
            while (true)
            {
                if (isPlayingAnimation || animationRepeat <= 0)
                {
                    yield return waitEndOfFrame;
                    continue;
                }
                animationRepeat--;
                isPlayingAnimation = true;

                float animDuration = climbAnimClips[animIndex].length / playerAnim.speed;
                playerAnim.Play(climbAnimClips[animIndex].name, -1, 0f);
                animIndex = (animIndex + 1) % climbAnimClips.Count;

                yield return new WaitForSeconds(animDuration);
                yield return waitEndOfFrame;

                isPlayingAnimation = false;
            }
        }

        private void DisableIsPlayingAnimation()
        {
            isPlayingAnimation = false;
        }
    }
}
