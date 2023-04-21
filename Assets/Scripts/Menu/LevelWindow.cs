using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWindow : MonoBehaviour
{
    public int levelIndex;

    private void OnEnable()
    {
        SceneManager.LoadSceneAsync(levelIndex);
    }
}
