using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ExitWindow : MonoBehaviour
{
    [Header("MainMenu panel")]
    [SerializeField] GameObject mainMenu_Panel;

    [Header("Buttons")]
    [SerializeField] Button yes_Btn;
    [SerializeField] Button no_Btn;

    MyInput input;

    private void OnEnable()
    {
        yes_Btn.onClick.AddListener(OnYES);
        no_Btn.onClick.AddListener(OnNO);

        input = GameManager.Instance.input;
        input.Menu.Escape.performed += OnEscape;
    }
    private void OnDisable()
    {
        yes_Btn.onClick.RemoveListener(OnYES);
        no_Btn.onClick.RemoveListener(OnNO);

        input.Menu.Escape.performed -= OnEscape;
    }

    private void OnYES()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void OnNO()
    {
        mainMenu_Panel.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private void OnEscape(InputAction.CallbackContext context)
    {
        OnNO();
    }
}
