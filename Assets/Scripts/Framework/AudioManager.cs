using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;

    public static AudioManager Instance
    {
        get { return GetInstance(); }
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
    }

    private static AudioManager GetInstance()
    {
        if(instance == null)
        {
            GameObject audioManger = new GameObject("Audio Manager");
            audioManger.AddComponent<AudioManager>();
        }
        return instance;
    }

    public void SetVolume(AudioChannels audioChannel, float NormalizedVolume)
    {
        if (masterMixer == null) return;

        string audioChannelString = null;

        switch (audioChannel)
        {
            case AudioChannels.MUSIC:
                audioChannelString = "Music Volume";
                break;
            case AudioChannels.SFX:
                audioChannelString = "SFX Volume";
                break;
            case AudioChannels.MASTER:
                audioChannelString = "Master Volume";
                break;
        }

        if(audioChannelString != null) masterMixer.SetFloat(audioChannelString, VolumeToDB(NormalizedVolume));
    }

    private float VolumeToDB(float NormalizedVolume)
    {
        return Mathf.Lerp(-80, 0, NormalizedVolume);
    }
}

public enum AudioChannels
{
    MASTER,
    MUSIC,
    SFX
}