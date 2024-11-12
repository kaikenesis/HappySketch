using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    public EButtonType buttonType;
    public EUIType curUIType;

    private Button button;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        button = transform.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClicked);

            Navigation navi = new Navigation();
            navi.mode = Navigation.Mode.None;
            button.navigation = navi;
        }
    }

    public abstract void OnClicked();
}

public enum EUIType
{
    MainMenu,
    SelectLevel,
    Explain,
    InGame,
    Result,
    MAX
}

public enum EButtonType
{
    Start,
    SelectLevel,
    GameStart,
    Retry,
    MainMenu,
    MAX
}