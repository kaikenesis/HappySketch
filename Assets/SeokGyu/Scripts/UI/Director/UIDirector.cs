using UnityEngine;

public class UIDirector : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selectLevel;
    [SerializeField] private GameObject explain;
    [SerializeField] private GameObject inGame;
    [SerializeField] private GameObject resultScreen;
    private bool isDebug = false;

    private Canvas mainMenuCanvas;
    private Canvas selectLevelCanvas;
    private Canvas explainCanvas;
    private Canvas inGameCanvas;
    private Canvas resultScreenCanvas;

    public int[] scoreList { get; private set; }
    public ELevel curLevel;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        UIManager.Instance.uiDirector = this;

        mainMenuCanvas = mainMenu.GetComponent<Canvas>();
        selectLevelCanvas = selectLevel.GetComponent<Canvas>();
        explainCanvas = explain.GetComponent<Canvas>();
        inGameCanvas = inGame.GetComponent<Canvas>();
        resultScreenCanvas = resultScreen.GetComponent<Canvas>();

        mainMenuCanvas.enabled = true;
        selectLevelCanvas.enabled = false;
        explainCanvas.enabled = false;
        inGameCanvas.enabled = false;
        resultScreenCanvas.enabled = false;

        scoreList = new int[UIManager.Instance.playerNum];
        for (int i = 0; i < scoreList.Length; i++)
        {
            scoreList[i] = 0;
        }
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
        }

        switch (buttonType)
        {
            case EButtonType.Start:
                selectLevelCanvas.enabled = true;
                break;
            case EButtonType.SelectLevel:
                explainCanvas.enabled = true;
                explain.GetComponent<ExplainScene>().SetText(curLevel);
                break;
            case EButtonType.GameStart:
                inGame.GetComponent<InGameScene>().Activate();
                break;
            case EButtonType.Retry:
                break;
            case EButtonType.MainMenu:
                mainMenuCanvas.enabled = true;
                break;
        }
    }

    public void IncreaseScore(int playerNum, int score)
    {
        int num = playerNum - 1;
        scoreList[num] += score;
        inGame.GetComponent<InGameScene>().SetScore(num, scoreList[num]);
    }

    public void ActivateFever()
    {
        inGame.GetComponent<InGameScene>().ActivateFeverTime();
    }

    public void UpdateTimer()
    {
        inGame.GetComponent<InGameScene>().DecreaseTime();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "디버깅"))
        {
            isDebug = !isDebug;
        }

        if(isDebug == true)
        {
            if(GUI.Button(new Rect(0, 50, 100, 50), "시간 감소"))
            {
                inGame.GetComponent<InGameScene>().DecreaseTime();
            }
            if (GUI.Button(new Rect(0, 100, 100, 50), "점수 증가"))
            {
                inGame.GetComponent<InGameScene>().SetScore(0, 10);
                inGame.GetComponent<InGameScene>().SetScore(1, 30);
            }
            if (GUI.Button(new Rect(0, 150, 100, 50), "피버 타임"))
            {
                inGame.GetComponent<InGameScene>().ActivateFeverTime();
            }
        }
    }
}
