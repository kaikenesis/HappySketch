using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : ChangeUIButton
{
    [SerializeField] private GameObject mainMenuCamera;

    public override void OnClicked()
    {
        base.OnClicked();

        // ī�޶� ��ȯ : ����ȭ��ī�޶� -> �ΰ��� ī�޶� 2��
    }
}
