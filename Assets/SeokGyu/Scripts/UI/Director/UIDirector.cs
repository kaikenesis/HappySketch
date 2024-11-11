using UnityEngine;

public class UIDirector : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selectLevel;
    [SerializeField] private GameObject explain;
    [SerializeField] private GameObject inGame;
    private bool isDebug = false;
    
    public int[] scoreList { get; private set; }
    public ELevel curLevel;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        UIManager.Instance.uiDirector = this;

        mainMenu.GetComponent<Canvas>().enabled = true;
        selectLevel.GetComponent<Canvas>().enabled = false;
        explain.GetComponent<Canvas>().enabled = false;
        inGame.GetComponent<Canvas>().enabled = false;

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
                mainMenu.GetComponent<Canvas>().enabled = false;
                break;
            case EUIType.SelectLevel:
                selectLevel.GetComponent<Canvas>().enabled = false;
                break;
            case EUIType.Explain:
                explain.GetComponent<Canvas>().enabled = false;
                break;
            case EUIType.InGame:
                inGame.GetComponent<Canvas>().enabled = false;
                break;
        }

        switch (buttonType)
        {
            case EButtonType.Start:
                selectLevel.GetComponent<Canvas>().enabled = true;
                break;
            case EButtonType.SelectLevel:
                explain.GetComponent<Canvas>().enabled = true;
                explain.GetComponent<ExplainScene>().SetText(curLevel);
                break;
            case EButtonType.GameStart:
                inGame.GetComponent<InGameScene>().Activate();
                break;
            case EButtonType.Retry:
                break;
            case EButtonType.MainMenu:
                mainMenu.GetComponent<Canvas>().enabled = true;
                break;
        }
    }

    public void IncreaseScore(int playerNum, int score)
    {
        int num = playerNum - 1;
        scoreList[num] += score;
        inGame.GetComponent<InGameScene>().SetScore(num, scoreList[num]);
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
