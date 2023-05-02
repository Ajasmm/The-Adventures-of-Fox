using System.Collections;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
    [SerializeField] private GameplayMode gameplayMode;

    private void OnEnable()
    {
        if (gameplayMode == null) return;
        StartCoroutine(RegisterGameplayMode());
    }
    IEnumerator RegisterGameplayMode()
    {
        yield return null;
        GameManager.Instance.RegisterGamePlayMode(gameplayMode);
    }
}
