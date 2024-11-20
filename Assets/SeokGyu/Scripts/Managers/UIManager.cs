using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIDirector uiDirector;
    public int playerNum = 2;
    public int[] scores;
    public bool bPlayGame = false;

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("UIManager");
                instance = newGameObject.AddComponent<UIManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);

        scores = new int[playerNum];
        for (int i = 0; i < scores.Length; i++)
            scores[i] = 0;
    }
}
