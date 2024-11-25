
public class ChangeUIButton : BaseButton
{
    public override void OnClicked()
    {
        UIManager.Instance.uiDirector.ChangeUI(curUIType, buttonType);
        SoundManager.PlaySFX(ConstStrings.SFX_UIBUTTON);
    }
}