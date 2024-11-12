using UnityEngine;

public class UIDirector : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject selectLevelObject;
    [SerializeField] private GameObject explainObject;
    [SerializeField] private GameObject inGameObject;
    [SerializeField] private GameObject resultScreenObject;
    private bool bDebug = false;

    private Canvas mainMenuCanvas;
    private Canvas selectLevelCanvas;
    private Canvas explainCanvas;
    private Canvas inGameCanvas;
    private Canvas resultCanvas;

    private ExplainScene explain;
    private InGameScene inGame;

    public ELevel curLevel;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        UIManager.Instance.uiDirector = this;

        mainMenuCanvas = mainMenuObject.GetComponent<Canvas>();
        selectLevelCanvas = selectLevelObject.GetComponent<Canvas>();
        explainCanvas = explainObject.GetComponent<Canvas>();
        inGameCanvas = inGameObject.GetComponent<Canvas>();
        resultCanvas = resultScreenObject.GetComponent<Canvas>();

        explain = explainObject.GetComponent<ExplainScene>();
        inGame = inGameObject.GetComponent<InGameScene>();

        mainMenuCanvas.enabled = true;
        selectLevelCanvas.enabled = false;
        explainCanvas.enabled = false;
        inGameCanvas.enabled = false;
        resultCanvas.enabled = false;
    }

    public void ChangeUI(EUIType curUIType, EButtonType buttonType)
    {
        switch (curUIType)
        {
            case EUIType.MainMenu:
                mainMenuCanvas.enabled = false;
                break;
            case EUIType.SelectLevel:
                selectLevelCanvas.enabled = false;
                break;
            case EUIType.Explain:
                explainCanvas.enabled = false;
                break;
            case EUIType.InGame:
                inGameCanvas.enabled = false;
                break;
            case EUIType.Result:
                resultCanvas.enabled = false;
                break;
        }

        switch (buttonType)
        {
            case EButtonType.Start:
                selectLevelCanvas.enabled = true;
                break;
            case EButtonType.SelectLevel:
                explainCanvas.enabled = true;
                explain.SetText(curLevel);
                break;
            case EButtonType.GameStart:
                inGame.Activate();
                break;
            case EButtonType.Retry:
                // ���ӳ��� �ʱ�ȭ �� �����
                break;
            case EButtonType.MainMenu:
                mainMenuCanvas.enabled = true;
                // ���ӳ��� �ʱ�ȭ �� �����
                break;
        }
    }

    public void IncreaseScore(int playerNum, int score)
    {
        int num = playerNum - 1;
        UIManager.Instance.scores[num] += score;
        inGame.SetScore(num, UIManager.Instance.scores[num]);
    }

    public void ActivateFever()
    {
        inGame.ActivateFeverTime();
    }

    public void UpdateTimer()
    {
        inGame.DecreaseTime();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "�����"))
        {
            bDebug = !bDebug;
        }

        if(bDebug == true)
        {
            if(GUI.Button(new Rect(0, 50, 100, 50), "�ð� ����"))
            {
                UIManager.Instance.curTime--;
                inGame.DecreaseTime();
            }
            if (GUI.Button(new Rect(0, 100, 100, 50), "���� ����"))
            {
                IncreaseScore(1, 10);
                IncreaseScore(2, 30);
            }
            if (GUI.Button(new Rect(0, 150, 100, 50), "�ǹ� Ÿ��"))
            {
                inGame.ActivateFeverTime();
            }
        }
    }
}
