using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControl : MonoBehaviour
{
    [SerializeField] private PostProcessingControl postProcessingControl;
    [SerializeField] private NoteManager noteManager;
    [SerializeField] private UIDirector uIDirector;

    private bool isFeverEffect = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            Debug_GotoGameScene();

        if (Input.GetKeyDown(KeyCode.F2))
            isFeverEffect = !isFeverEffect;

        if (isFeverEffect)
            postProcessingControl.PlayFeverEffect();
        else
            postProcessingControl.StopFeverEffect();
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
