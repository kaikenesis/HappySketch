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

    private void Start()
    {
        SetInfo();
    }

    protected virtual void Init()
    {
        button = transform.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClicked);
        }
    }

    protected virtual void SetInfo()
    {

    }

    public virtual void OnClicked()
    {
        UIManager.Instance.ChangeUI(curUIType, buttonType);
    }
}

public enum EUIType
{
    MainMenu,
    SelectLevel,
    Explain,
    InGame,
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