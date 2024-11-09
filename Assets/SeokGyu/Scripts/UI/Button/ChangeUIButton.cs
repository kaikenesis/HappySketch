
using UnityEngine;

public class ChangeUIButton : BaseButton
{
    public override void OnClicked()
    {
        UIManager.Instance.ChangeUI(curUIType, buttonType);
    }
}