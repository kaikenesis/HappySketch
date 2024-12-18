using UnityEngine;

public class ChangeCameraButton : ChangeUIButton, ICameraChanger
{
    [SerializeField] private Camera mainCamera;

    public override void OnClicked()
    {
        base.OnClicked();

        ChangeCamera(buttonType);
    }

    public void ChangeCamera(EButtonType type)
    {
        // 카메라 전환 : 메인화면카메라 <-> 인게임 카메라 2개
        switch (type)
        {
            case EButtonType.MainMenu:
                {
                    mainCamera.enabled = true;
                    GameController.Instance.GetPlayerCamera(1).enabled = false;
                    GameController.Instance.GetPlayerCamera(2).enabled = false;
                }
                break;
            case EButtonType.Start:
                {
                    mainCamera.enabled = false;
                    GameController.Instance.GetPlayerCamera(1).enabled = true;
                    GameController.Instance.GetPlayerCamera(2).enabled = true;
                }
                break;
        }
    }
}
