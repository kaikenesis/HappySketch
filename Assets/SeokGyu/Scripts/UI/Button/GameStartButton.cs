
using UnityEngine;

public class GameStartButton : BaseButton
{
    private GameObject selectLevelUI;
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

        selectLevelUI = GameObject.Find("SelectLevel");
    }

    protected override void SetInfo()
    {
        base.SetInfo();
    }

    public override void OnClicked()
    {

    }
}
