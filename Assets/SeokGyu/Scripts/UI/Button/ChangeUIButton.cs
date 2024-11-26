
public class ChangeUIButton : BaseButton
{
    public override void OnClicked()
    {
        UIManager.Instance.uiDirector.ChangeUI(curUIType, buttonType);
        SoundManager.PlaySFX(AudioNameTag.SFX_UIBUTTON);
    }
}