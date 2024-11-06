using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    [SerializeField]
    public EButtonType buttonType { get; private set; }

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
            button.onClick.AddListener(SetInfo);
        }
    }

    protected virtual void SetInfo()
    {

    }

    public abstract void OnClicked();
}

public enum EButtonType
{
    Tutorial,
    LevelSelect,
    GameStart,
    Retry,
    MainMenu
}