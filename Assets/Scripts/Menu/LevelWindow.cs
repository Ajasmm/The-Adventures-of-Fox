using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class LevelWindow : MonoBehaviour
{
    [Header("MainMenu panel")]
    [SerializeField] GameObject mainMenu_Panel;

    [Header("Level Button")]
    [SerializeField] List<LevelButton> levelButtonList;

    MyInput input;

    public void ButtonClicked()
    {
        for (int i = 0; i < levelButtonList.Count; i++)
            levelButtonList[i].SetInteratinMode(false);
    }

    private void OnEnable()
    {
        /*
         * To Update buttons index and text in the editor
         * add buttons to the list and just disable and enable this script
        
        for(int i =0; i < levelButtonList.Count;)
        {
            levelButtonList[i].SetIndex(++i);
        }

        */

        input = GameManager.Instance.input;
        input.Menu.Escape.performed += OnEscape;

        LevelData leveldata = GameManager.Instance.levelData;
        for(int i =0; i < levelButtonList.Count; i++)
        {
            if (i < leveldata.levelsCompleted) levelButtonList[i].SetInteratinMode(true);
            else levelButtonList[i].SetInteratinMode(false);
        }
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
