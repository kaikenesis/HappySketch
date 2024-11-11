
using UnityEngine;

public class ChangeUIButton : BaseButton
{
    [SerializeField] private GameObject uiDirectorObject;
    protected UIDirector uiDirector { get; private set; }

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        uiDirector = uiDirectorObject.GetComponent<UIDirector>();
    }

    public override void OnClicked()
    {
        uiDirector.ChangeUI(curUIType, buttonType);
    }
}