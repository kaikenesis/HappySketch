
using UnityEngine;

public class StartButton : BaseButton
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
        base.OnClicked();

    }
}
