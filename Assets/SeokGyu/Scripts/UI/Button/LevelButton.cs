
public class LevelButton : BaseButton
{
    private void Awake()
    {
        Init();
    }

    void Start()
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
        // 키설명 화면으로 전환
    }
}
