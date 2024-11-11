using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : ChangeUIButton
{
    [SerializeField] private GameObject mainMenuCamera;

    public override void OnClicked()
    {
        base.OnClicked();

        // 카메라 전환 : 메인화면카메라 -> 인게임 카메라 2개
    }
}
