using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;

    public static AudioManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }
    private static AudioManager instance;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if (instance != this) { Destroy(gameObject); }

        StartCoroutine(InitializeVolume());
    }

    private IEnumerator InitializeVolume()
    {
        yield return null;

        SetVolume(AudioChannels.MASTER, 1);
        SetVolume(AudioChannels.MUSIC, GameManager.Instance.settingsData.musicVolume);
        SetVolume(AudioChannels.SFX, GameManager.Instance.settingsData.sfxVolume);

    }

    public void SetVolume(AudioChannels audioChannel, float NormalizedVolume)
    {
        if (masterMixer == null)
        {
            Debug.LogError("No mixer");
            return;
        }

        string audioChannelString = null;

        switch (audioChannel)
        {
            case AudioChannels.MUSIC:
                audioChannelString = "Music Volume";
                GameManager.Instance.settingsData.musicVolume = NormalizedVolume;
                break;
            case AudioChannels.SFX:
                audioChannelString = "SFX Volume";
                GameManager.Instance.settingsData.sfxVolume = NormalizedVolume;
                break;
            case AudioChannels.MASTER:
                audioChannelString = "Master Volume";
                break;
        }

        SaveSystem.SetSettingsData(GameManager.Instance.settingsData);
        if(audioChannelString != null) masterMixer.SetFloat(audioChannelString, VolumeToDB(NormalizedVolume));
    }

    private float VolumeToDB(float NormalizedVolume)
    {
        return Mathf.Log10(NormalizedVolume) * 20;
    }
}

public enum AudioChannels
{
    MASTER,
    MUSIC,
    SFX
}