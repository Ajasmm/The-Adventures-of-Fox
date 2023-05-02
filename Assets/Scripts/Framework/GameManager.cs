using System.Collections;
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

    public LevelData levelData;
    public SettingsData settingsData;

    private void Awake()
    {
        input = new MyInput();
    }
    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if (instance != this) Destroy(gameObject);

        StartCoroutine(Initialize());
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            isGameFinished = true;
            SaveSystem.SetLevelData(levelData);
            SaveSystem.SetSettingsData(settingsData);
        }
    }


    private IEnumerator Initialize()
    {
        while (AudioManager.Instance == null) yield return null;
       
        levelData = SaveSystem.GetLevelData();
        settingsData = SaveSystem.GetSettingsData();
    }
    private static GameManager GetInstance()
    {
        if (isGameFinished) return null;

        if (instance == null)
        {
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManagerObj.AddComponent<GameManager>();
        }
        return instance;
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
