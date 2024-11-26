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
        // ī�޶� ��ȯ : ����ȭ��ī�޶� -> �ΰ��� ī�޶� 2��
        mainCamera.enabled = true;
        GameController.Instance.GetPlayerCamera(1).enabled = false;
        GameController.Instance.GetPlayerCamera(2).enabled = false;
    }
}
