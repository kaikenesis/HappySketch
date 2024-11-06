
public class MainMenuButton : BaseButton
{
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        SetInfo();
    }

    protected override void Init()
    {
        base.Init();
    }

    protected override void SetInfo()
    {
        base.SetInfo();
    }

    public override void OnClicked()
    {
        // 메인 화면으로 전환
    }
}
