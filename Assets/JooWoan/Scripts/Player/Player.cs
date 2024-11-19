using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappySketch
{
    public class Player : MonoBehaviour
    {
        public Camera PlayerCam => playerCam;
        public CameraFollow CameraControl => cameraFollow;

        [SerializeField] private int playerNumber;
        [SerializeField] private float animationSpeed;
        [SerializeField] private AnimationClip idleClip;
        [SerializeField] private List<AnimationClip> climbAnimClips;
        [SerializeField] private Camera playerCam;

        //////////////////////// ERASE
        [SerializeField] private KeyCode moveKey;
        ////////////////////////

        private WaitForEndOfFrame waitEndOfFrame = new WaitForEndOfFrame();
        private Animator playerAnim;
        private CameraFollow cameraFollow;

        private Vector3 initialPos;
        private Quaternion initialRotation;

        private int animIndex = 0;
        int animationRepeat = 0;

        private bool isPlayingAnimation = false;

        void Start()
        {
            playerAnim = GetComponent<Animator>();
            playerAnim.speed = animationSpeed;

            initialPos = transform.position;
            initialRotation = transform.rotation;

            cameraFollow = playerCam.GetComponent<CameraFollow>();

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

        public void ResetTransform()
        {
            transform.rotation = initialRotation;
            transform.position = initialPos;
        }

        public void ResetAnimation()
        {
            ClearAnimationRepeat();
            playerAnim.Play(idleClip.name, -1, 0f);
            animIndex = 0;
        }
    }
}
