using UnityEngine;

public class UIDirector : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject selectLevelUI;
    [SerializeField] private ExplainUI explainUI;
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private ResultUI resultUI;
    private bool bDebug = true;

    public ELevel curLevel;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
    {
        UIManager.Instance.uiDirector = this;

        mainMenuUI.SetActive(true);
        selectLevelUI.SetActive(false);
        explainUI.Deactivate();
        inGameUI.Deactivate();
        resultUI.Deactivate();
    }

    private void CompareScore()
    {
        int max = UIManager.Instance.scores[0];
        int playerNum = 0;
        for (int i = 1; i < UIManager.Instance.playerNum; i++)
        {
            if (max < UIManager.Instance.scores[i])
            {
                max = UIManager.Instance.scores[i];
                playerNum = i;
            }
        }

        resultUI.SetWinner(playerNum);
    }

    public void ChangeUI(EUIType curUIType, EButtonType buttonType)
    {
        ActivateUI(buttonType);
        DeactivateUI(curUIType);
    }

    private void DeactivateUI(EUIType curUIType)
    {
        switch (curUIType)
        {
            case EUIType.MainMenu:
                mainMenuUI.SetActive(false);
                break;
            case EUIType.SelectLevel:
                selectLevelUI.SetActive(false);
                break;
            case EUIType.Explain:
                explainUI.Deactivate();
                break;
            case EUIType.InGame:
                inGameUI.Deactivate();
                break;
            case EUIType.Result:
                resultUI.Deactivate();
                break;
        }
    }

    private void ActivateUI(EButtonType buttonType)
    {
        switch (buttonType)
        {
            case EButtonType.Start:
                selectLevelUI.SetActive(true);
                break;
            case EButtonType.SelectLevel:
                explainUI.Activate();
                explainUI.SetText(curLevel);
                break;
            case EButtonType.GameStart:
                inGameUI.Activate();
                GameController.Instance.SetBackgroundDome(BgDomeType.REALISTIC, true);
                GameController.Instance.SetBackgroundDome(BgDomeType.TOON, false);
                break;
            case EButtonType.Retry:
                inGameUI.ResetGame();
                inGameUI.Activate();
                break;
            case EButtonType.MainMenu:
                GameController.Instance.SetBackgroundDome(BgDomeType.REALISTIC, false);
                GameController.Instance.SetBackgroundDome(BgDomeType.TOON, true);
                SoundManager.PlayBGM(AudioNameTag.BGM_TITLE);
                inGameUI.ResetGame();
                inGameUI.Deactivate();
                mainMenuUI.SetActive(true);
                break;
        }
    }

    public void IncreaseScore(int playerNum, int score)
    {
        if (UIManager.Instance.bPlayGame == false) return;

        int num = playerNum - 1;
        UIManager.Instance.scores[num] += score;
        inGameUI.UpdateScore(num, UIManager.Instance.scores[num]);
    }

    public void ActivateFever()
    {
        if (UIManager.Instance.bPlayGame == false) return;
        inGameUI.ActivateFeverTime();
    }

    public void FinishGame()
    {
        resultUI.Activate();
        CompareScore();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "디버깅"))
        {
            bDebug = !bDebug;
        }

        if (bDebug == true)
        {
            if (GUI.Button(new Rect(0, 50, 100, 50), "시간 감소"))
            {
                NoteManager.Instance.curTime++;
                inGameUI.DecreaseTime();
            }
            if (GUI.Button(new Rect(0, 100, 100, 50), "남은시간 60초"))
            {
                NoteManager.Instance.curTime = 0;
                inGameUI.DecreaseTime();
            }
            if (GUI.Button(new Rect(0, 150, 100, 50), "남은시간 10초"))
            {
                NoteManager.Instance.curTime = 50;
                inGameUI.DecreaseTime();
            }
            if (GUI.Button(new Rect(0, 200, 100, 50), "1p점수 + 1000"))
            {
                IncreaseScore(1, 1000);
            }
            if (GUI.Button(new Rect(0, 250, 100, 50), "2p점수 + 1000"))
            {
                IncreaseScore(2, 1000);
            }
        }
    }
}
