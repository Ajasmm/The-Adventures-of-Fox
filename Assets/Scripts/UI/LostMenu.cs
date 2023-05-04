using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LostMenu : MonoBehaviour 
{ 
    [SerializeField] Button restart_Btn;
    [SerializeField] Button menu_Btn;

    private void OnEnable()
    {
        SetActiveButtons(true);

        if (restart_Btn) restart_Btn.onClick.AddListener(OnRestart);
        if (menu_Btn) menu_Btn.onClick.AddListener(OnMenu);
    }
    private void OnDisable()
    {
        if (restart_Btn) restart_Btn.onClick.RemoveListener(OnRestart);
        if (menu_Btn) menu_Btn.onClick.RemoveListener(OnMenu);
    }

    private void OnRestart()
    {
        SetActiveButtons(false);
        GameManager.Instance.GameplayMode?.OnRestart();
    }
    private void OnMenu()
    {
        SetActiveButtons(false);
        SceneManager.LoadSceneAsync(0);
    }

    private void SetActiveButtons(bool state)
    {
        if(restart_Btn) restart_Btn.interactable = state;
        if(menu_Btn) menu_Btn.interactable = state;
    }


}
