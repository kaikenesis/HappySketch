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
        // ī�޶� ��ȯ : ����ȭ��ī�޶� <-> �ΰ��� ī�޶� 2��
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
