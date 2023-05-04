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

    public float masterVolume = 1, musicVolume = 1, sfxVolume = 1;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if (instance != this) { Destroy(gameObject); }

        StartCoroutine(InitializeVolume());
    }
    private void OnDestroy()
    {
        Save();
    }

    private IEnumerator InitializeVolume()
    {
        GameManager gameManager = GameManager.Instance;
        yield return null;

        SetVolume(AudioChannels.MASTER, 1);
        SetVolume(AudioChannels.MUSIC, musicVolume);
        SetVolume(AudioChannels.SFX, sfxVolume);

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
                musicVolume = NormalizedVolume;
                break;
            case AudioChannels.SFX:
                audioChannelString = "SFX Volume";
                sfxVolume = NormalizedVolume;
                break;
            case AudioChannels.MASTER:
                audioChannelString = "Master Volume";
                masterVolume = NormalizedVolume;
                break;
        }


        if(audioChannelString != null) masterMixer.SetFloat(audioChannelString, VolumeToDB(NormalizedVolume));
    }
    private float VolumeToDB(float NormalizedVolume)
    {
        return Mathf.Log10(NormalizedVolume) * 20;
    }

    public static void Save()
    {
        SaveSystem.SaveSettingsData();
    }
}

public enum AudioChannels
{
    MASTER,
    MUSIC,
    SFX
}