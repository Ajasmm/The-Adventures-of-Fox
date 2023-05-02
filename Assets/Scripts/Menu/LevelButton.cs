using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;

    [SerializeField] private int levelIndex;
    [SerializeField] private LevelWindow levelWindow;

    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        GameManager.Instance.levelData.levelPlaying = levelIndex;
        SceneManager.LoadSceneAsync("Level_" + levelIndex);
        levelWindow.ButtonClicked();
    }
    public void SetIndex(int index)
    {
        levelIndex = index;
        text.text = levelIndex.ToString();
    }
    public void SetInteratinMode(bool interatinMode)
    {
        button.interactable = interatinMode;
    }
}
