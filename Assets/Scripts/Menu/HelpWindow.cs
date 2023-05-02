using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HelpWindow : MonoBehaviour
{
    [SerializeField] GameObject mainMenu_Panel;

    MyInput input;

    private void OnEnable()
    {
        input = GameManager.Instance.input;
        input.Menu.Escape.performed += OnEscape;
    }
    private void OnDisable()
    {
        input.Menu.Escape.performed -= OnEscape; 
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        mainMenu_Panel.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
