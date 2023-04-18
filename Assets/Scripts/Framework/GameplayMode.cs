using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayMode : MonoBehaviour
{
    [SerializeField] GameObject gameplay_UI;
    [SerializeField] GameObject pauseMenu_UI;
    [SerializeField] GameObject win_UI;
    [SerializeField] GameObject loss_UI;

    public static bool isPlaying = false;

    private MyInput input;

    private void OnDisable()
    {
        OnStop();
    }

    public void OnStart()
    {
        Time.timeScale = 0f;
        isPlaying = false;
        DisableUI();

        input = GameManager.Instance.input;
        input.Disable();

        OnPlay();
    }
    public void OnPlay()
    {
        Time.timeScale = 1;
        isPlaying = true;
        DisableUI();

        if(gameplay_UI) gameplay_UI.SetActive(true);

        input.GamePlay.Escape.performed += OnEscape;
        input.GamePlay.Enable();
    }
    public void OnPause()
    {
        Time.timeScale = 0;
        isPlaying = false;
        DisableUI();

        if (pauseMenu_UI) pauseMenu_UI.SetActive(true);

        input.Disable();
        input.Menu.Enable();
    }
    public void OnResume()
    {
        Time.timeScale = 1;
        isPlaying = true;
        DisableUI();

        if (gameplay_UI) gameplay_UI.SetActive(true);

        input.Disable();
        input.GamePlay.Enable();
    }
    public void OnStop()
    {
        Time.timeScale = 1;
        isPlaying = false;
        DisableUI();

        input.GamePlay.Escape.performed -= OnEscape;
        input.Disable();
        input.Menu.Enable();
    }
    public void OnWin()
    {
        OnStop();
        if(win_UI) win_UI.SetActive(true);
    }
    public void OnLoss()
    {
        OnStop();
        if(loss_UI) loss_UI.SetActive(true);
    }

    private void DisableUI()
    {
        if(gameplay_UI) gameplay_UI.SetActive(false);
        if(pauseMenu_UI) pauseMenu_UI.SetActive(false);
        if(win_UI) win_UI.SetActive(false);
        if(loss_UI) loss_UI.SetActive(false);
    }
    private void OnEscape(InputAction.CallbackContext context)
    {
        OnPause();
    }
}
