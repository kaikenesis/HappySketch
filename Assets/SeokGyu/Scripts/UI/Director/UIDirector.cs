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
    private ResultScene resultScene;

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
        resultScene = resultScreenObject.GetComponent<ResultScene>();

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
                inGame.ResetGame();
                inGame.Activate();
                break;
            case EButtonType.MainMenu:
                inGame.ResetGame();
                inGameCanvas.enabled = false;
                mainMenuCanvas.enabled = true;
                break;
        }
    }

    public void IncreaseScore(int playerNum, int score)
    {
        if (UIManager.Instance.bPlayGame == false) return;

        int num = playerNum - 1;
        UIManager.Instance.scores[num] += score;
        inGame.SetScore(num, UIManager.Instance.scores[num]);
    }

    public void ActivateFever()
    {
        if (UIManager.Instance.bPlayGame == false) return;

        inGame.ActivateFeverTime();
    }

    public void UpdateTimer()
    {
        if (UIManager.Instance.bPlayGame == false) return;

        inGame.DecreaseTime();
    }

    public void FinishGame()
    {
        resultCanvas.enabled = true;
        CompareScore();
    }

    void CompareScore()
    {
        int max = UIManager.Instance.scores[0];
        int playerNum = 0;
        for(int i =1;i<UIManager.Instance.playerNum;i++)
        {
            if(max < UIManager.Instance.scores[i])
            {
                max = UIManager.Instance.scores[i];
                playerNum = i;
            }
        }

        resultScene.SetWinner(playerNum);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "디버깅"))
        {
            bDebug = !bDebug;
        }

        if(bDebug == true)
        {
            if(GUI.Button(new Rect(0, 50, 100, 50), "시간 감소"))
            {
                UIManager.Instance.curTime--;
                inGame.DecreaseTime();
            }
            if (GUI.Button(new Rect(0, 100, 100, 50), "점수 증가"))
            {
                IncreaseScore(1, 10);
                IncreaseScore(2, 30);
            }
            if (GUI.Button(new Rect(0, 150, 100, 50), "피버 타임"))
            {
                inGame.ActivateFeverTime();
            }
            if (GUI.Button(new Rect(0, 200, 100, 50), "1p승리"))
            {
                UIManager.Instance.curTime = 5;
                IncreaseScore(1, 1000);
                inGame.DecreaseTime();
            }
            if (GUI.Button(new Rect(0, 250, 100, 50), "2p승리"))
            {
                UIManager.Instance.curTime = 5;
                IncreaseScore(2, 1000);
                inGame.DecreaseTime();
            }
        }
    }
}
