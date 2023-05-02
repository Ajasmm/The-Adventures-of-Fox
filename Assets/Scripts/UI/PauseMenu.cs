using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button resume_Btn;
    [SerializeField] Button restart_Btn;
    [SerializeField] Button menu_Btn;

    MyInput input;

    private void OnEnable()
    {
        SetActiveButtons(true);

        if (resume_Btn) resume_Btn.onClick.AddListener(OnResume);
        if (restart_Btn) restart_Btn.onClick.AddListener(OnRestart);
        if (menu_Btn) menu_Btn.onClick.AddListener(OnMenu);

        input = GameManager.Instance.input;
        input.Menu.Escape.performed += OnEscape;
    }
    private void OnDisable()
    {
        if (resume_Btn) resume_Btn.onClick.RemoveListener(OnResume);
        if (restart_Btn) restart_Btn.onClick.RemoveListener(OnResume);
        if (menu_Btn) menu_Btn.onClick.RemoveListener(OnResume);
        
        input.Menu.Escape.performed -= OnEscape;
    }


    private void OnResume()
    {
        SetActiveButtons(false);

        GameplayMode currentGameplayMode = GameManager.Instance.GameplayMode;
        currentGameplayMode?.OnResume();
    }
    private void OnRestart()
    {
        SetActiveButtons(false);

        SceneManager.LoadSceneAsync(this.gameObject.scene.buildIndex);
    }
    private void OnMenu()
    {
        SetActiveButtons(false);
        SceneManager.LoadSceneAsync(0);
    }

    private void SetActiveButtons(bool state)
    {
        if (resume_Btn) resume_Btn.interactable = state;
        if (restart_Btn) restart_Btn.interactable = state;
        if (menu_Btn) menu_Btn.interactable = state;
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        OnResume();
    }

}
