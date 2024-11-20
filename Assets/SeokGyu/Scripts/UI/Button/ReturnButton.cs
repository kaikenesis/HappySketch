using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : ChangeUIButton
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

        // ī�޶� ��ȯ : ����ȭ��ī�޶� -> �ΰ��� ī�޶� 2��
        mainCamera.enabled = true;
        GameController.Instance.GetPlayerCamera(1).enabled = false;
        GameController.Instance.GetPlayerCamera(2).enabled = false;
    }
}