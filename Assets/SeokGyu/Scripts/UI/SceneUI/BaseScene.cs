using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        canvas = GetComponent<Canvas>();
    }

    public virtual void Activate()
    {
        canvas.enabled = true;
    }

    public virtual void Deactivate()
    {
        canvas.enabled = false;
    }
}
