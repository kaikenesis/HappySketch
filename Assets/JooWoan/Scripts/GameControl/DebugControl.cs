using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControl : MonoBehaviour
{
    [SerializeField] private PostProcessingControl postProcessingControl;
    [SerializeField] private FireEffects fireEffects;
    [SerializeField] private NoteManager noteManager;
    [SerializeField] private UIDirector uIDirector;

    private bool isFeverEffect = false;
    private bool isFireEnabled = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            Debug_GotoGameScene();

        if (Input.GetKeyDown(KeyCode.F2))
        {
            isFeverEffect = !isFeverEffect;
            if (isFeverEffect)
                postProcessingControl.PlayFeverEffect();
            else
                postProcessingControl.StopFeverEffect();
        }

        if (Input.GetKeyDown(KeyCode.F3))
            GameController.Instance.ResetLevel();

        if (Input.GetKeyDown(KeyCode.F4))
        {
            isFireEnabled = !isFireEnabled;

            if (isFireEnabled)
                fireEffects.EnableFireEffect();
            else
                fireEffects.DisableFireEffect();
        }

        if (Input.GetKeyDown(KeyCode.F5))
            SoundManager.PlaySFX("Blunt6");

        if (Input.GetKeyDown(KeyCode.F6))
            SoundManager.PlayBGM("CherryCola");

        if (Input.GetKeyDown(KeyCode.F7))
            SoundManager.SetBgmSpeed(1.5f);

        if (Input.GetKeyDown(KeyCode.F8))
        {
            SoundManager.PauseBGM();
            SoundManager.SetBgmSpeed();
        }
    }
    private void Debug_GotoGameScene()
    {
        GameController.Instance.GetPlayerCamera(1).enabled = true;
        GameController.Instance.GetPlayerCamera(2).enabled = true;

        noteManager.gameObject.SetActive(false);
        uIDirector.gameObject.SetActive(false);

        GameController.Instance.SetBackgroundDome(BgDomeType.TOON, false);
        GameController.Instance.SetBackgroundDome(BgDomeType.REALISTIC, true);
    }
}
