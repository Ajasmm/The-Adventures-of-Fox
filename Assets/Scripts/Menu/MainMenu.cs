using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button play;
    [SerializeField] Button settings;
    [SerializeField] Button help;
    [SerializeField] Button exit;

    [Header("Windows")]
    [SerializeField] GameObject levels_Window;
    [SerializeField] GameObject settings_Window;
    [SerializeField] GameObject help_Window;
    [SerializeField] GameObject exit_Window;

    private void OnEnable()
    {
        play.onClick.AddListener(OnPlay);
        settings.onClick.AddListener(OnSettings);
        help.onClick.AddListener(OnHelp);
        exit.onClick.AddListener(OnExit);

        MyInput input = GameManager.Instance.input;
        input.Disable();
        input.Menu.Enable();
    }
    private void OnDisable()
    {
        play.onClick.RemoveListener(OnPlay);
        settings.onClick.RemoveListener(OnSettings);
        help.onClick.RemoveListener(OnHelp);
        exit.onClick.RemoveListener(OnExit);
    }

    private void OnPlay()
    {
        levels_Window.SetActive(true);
        DisableThisWindow();
    }
    private void OnSettings()
    {
        settings_Window.SetActive(true);
        DisableThisWindow();
    }
    private void OnHelp()
    {
        help_Window.SetActive(true);
        DisableThisWindow();
    }
    private void OnExit()
    {
        exit_Window.SetActive(true);
        DisableThisWindow();
    }
    private void DisableThisWindow()
    {
        this.gameObject.SetActive(false);
    }
}
