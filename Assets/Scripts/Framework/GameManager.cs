using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get { return GetInstance(); }
    }
    private static GameManager instance;

    public GameplayMode GameplayMode { 
        get { return gameplayMode; }
    }
    private GameplayMode gameplayMode;

    public GameObject player { get; private set; }

    public MyInput input;

    private static bool isGameFinished = false;

    private void Awake()
    {
        input = new MyInput();
    }
    private void OnEnable()
    {
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
            DontDestroyOnLoad(gameObject);
            AudioManager.Instance.SetVolume(AudioChannels.MASTER, 1.0f);
        }else if (GameManager.instance != this) Destroy(gameObject);
    }
    private void OnDestroy()
    {
        isGameFinished = true;
    }

    private static GameManager GetInstance()
    {
        if (isGameFinished) return null;

        if (instance == null)
        {
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManagerObj.AddComponent<GameManager>();
        }
        return GameManager.instance;
    }
    public void RegisterGamePlayMode(GameplayMode gameplayMode)
    {
        this.gameplayMode?.OnStop();
        this.gameplayMode = gameplayMode;
        this.gameplayMode?.OnStart();
    }
    public void RegisterPlayer(GameObject player)
    {
        this.player = player;
    }
}
