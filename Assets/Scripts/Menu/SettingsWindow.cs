using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [Header("MainMenu panel")]
    [SerializeField] GameObject mainMenu_Panel;

    [Header("Audio Sliders")]
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider sfxVolume;

    MyInput input;

    private void OnEnable()
    {
        musicVolume.onValueChanged.AddListener(OnMusicVolumeChange);
        sfxVolume.onValueChanged.AddListener(OnSFXVolumeChange);

        input = GameManager.Instance.input;
        input.Menu.Escape.performed += OnEscape;
    }
    private void OnDisable()
    {
        musicVolume.onValueChanged.AddListener(OnMusicVolumeChange);
        sfxVolume.onValueChanged.RemoveListener(OnSFXVolumeChange);

        input.Menu.Escape.performed -= OnEscape;
    }

    private void OnMusicVolumeChange(float volume)
    {
        AudioManager.Instance.SetVolume(AudioChannels.MUSIC, volume);
    }
    private void OnSFXVolumeChange(float volume)
    {
        AudioManager.Instance.SetVolume(AudioChannels.SFX, volume);
    }
    private void OnEscape(InputAction.CallbackContext context)
    {
        mainMenu_Panel.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
