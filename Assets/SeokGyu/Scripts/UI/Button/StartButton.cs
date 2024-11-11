using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : ChangeUIButton
{
    [SerializeField] private GameObject mainMenuCamera;
    private Camera mainCamera;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        mainCamera = mainMenuCamera.GetComponent<Camera>();
    }

    public override void OnClicked()
    {
        base.OnClicked();

        // 카메라 전환 : 메인화면카메라 -> 인게임 카메라 2개
        mainCamera.enabled = false;
        GameController.Instance.GetPlayerCamera(1).enabled = true;
        GameController.Instance.GetPlayerCamera(2).enabled = true;
    }
}
