using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    enum EButtonType
    {
        Tutorial,
        LevelSelect,
        GameStart,
        Retry,
        MainMenu
    }

    private Button button;

    private void Awake()
    {
        Init();
    }

    void Start()
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
