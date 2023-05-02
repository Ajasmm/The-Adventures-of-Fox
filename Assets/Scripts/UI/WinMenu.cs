using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] bool isLastLevel;
    [SerializeField] TMP_Text text;

    [SerializeField] Button restart_Btn;
    [SerializeField] Button next_Btn;
    [SerializeField] Button menu_Btn;

    [Header("Only for Last Win Screen")]
    [SerializeField] ParticleSystem particleSystemObj;
    [SerializeField] Image bg;


    private void OnEnable()
    {
        if(isLastLevel)
        {
            text.text = "You successfuly completed the game.\nCongradulations.";
            next_Btn.gameObject.SetActive(false);
            StartCoroutine(StartFinalWinAnimation());
            return;
        }
        else
        {
            GameManager.Instance.levelData.levelPlaying = GameManager.Instance.levelData.levelsCompleted = level + 1;
        }

        SetActiveButtons(true);

        if (restart_Btn) restart_Btn.onClick.AddListener(OnRestart);
        if (next_Btn && !isLastLevel) next_Btn.onClick.AddListener(OnNext);
        if (menu_Btn) menu_Btn.onClick.AddListener(OnMenu);
    }
    private void OnDisable()
    {
        if (restart_Btn) restart_Btn.onClick.RemoveListener(OnRestart);
        if (next_Btn && !isLastLevel) next_Btn.onClick.RemoveListener(OnNext);
        if (menu_Btn) menu_Btn.onClick.RemoveListener(OnMenu);
    }

    private void OnRestart()
    {
        SetActiveButtons(false);

        SceneManager.LoadSceneAsync(this.gameObject.scene.buildIndex);
    }
    private void OnNext()
    {
        SetActiveButtons(false);
        SceneManager.LoadSceneAsync("Level_" + (level + 1));
    }
    private void OnMenu()
    {
        SetActiveButtons(false);

        SceneManager.LoadSceneAsync(1);
    }

    private void SetActiveButtons(bool state)
    {
        if(restart_Btn) restart_Btn.interactable = state;
        if(next_Btn) next_Btn.interactable = state;
        if(menu_Btn) menu_Btn.interactable = state;
    }
    IEnumerator StartFinalWinAnimation()
    {
        particleSystemObj.Play();

        bg.enabled = false;
        restart_Btn.gameObject.SetActive(false);
        menu_Btn.gameObject.SetActive(false);

        while (particleSystemObj.isPlaying) yield return null; 
        
        bg.enabled = true;
        restart_Btn.gameObject.SetActive(true);
        menu_Btn.gameObject.SetActive(true);

        SetActiveButtons(true);

        if (restart_Btn) restart_Btn.onClick.AddListener(OnRestart);
        if (next_Btn && !isLastLevel) next_Btn.onClick.AddListener(OnNext);
        if (menu_Btn) menu_Btn.onClick.AddListener(OnMenu);
    }

}
