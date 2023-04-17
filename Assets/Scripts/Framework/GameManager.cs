using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get { return GetInstance(); }
    }
    private static GameManager instance;

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
}
