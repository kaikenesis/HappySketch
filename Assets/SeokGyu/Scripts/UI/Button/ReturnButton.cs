using UnityEngine;

public class ReturnButton : ChangeUIButton, ICameraChanger
{
    [SerializeField] private Camera mainCamera;

    public override void OnClicked()
    {
        base.OnClicked();

        ChangeCamera();
    }

    public void ChangeCamera()
    {
        // 카메라 전환 : 메인화면카메라 -> 인게임 카메라 2개
        mainCamera.enabled = true;
        GameController.Instance.GetPlayerCamera(1).enabled = false;
        GameController.Instance.GetPlayerCamera(2).enabled = false;
    }
}
