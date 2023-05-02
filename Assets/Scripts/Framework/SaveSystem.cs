using UnityEngine;

public static class SaveSystem
{
    static string settingsKey = "SettingsData";
    static string levelKey = "LevelData";

    static string jsonData;

    static SettingsData settingsData;
    static LevelData levelData;

    public static SettingsData GetSettingsData()
    {
        if (PlayerPrefs.HasKey(settingsKey))
        {
            jsonData = PlayerPrefs.GetString(settingsKey);
            settingsData = JsonUtility.FromJson<SettingsData>(jsonData);
        }
        else settingsData = new SettingsData(1, 1);
        
        return settingsData;
    }
    public static LevelData GetLevelData()
    {
        if (PlayerPrefs.HasKey(levelKey))
        {
            jsonData = PlayerPrefs.GetString(levelKey);
            levelData = JsonUtility.FromJson<LevelData>(jsonData);
        }
        else levelData = new LevelData(true);

        return levelData;
    }

    public static void SetSettingsData(SettingsData settingsData)
    {
        jsonData = JsonUtility.ToJson(settingsData);
        PlayerPrefs.SetString(settingsKey, jsonData);
        PlayerPrefs.Save();
    }
    public static void SetLevelData(LevelData levelData)
    {
        jsonData = JsonUtility.ToJson(levelData);
        PlayerPrefs.SetString(levelKey, jsonData);
        PlayerPrefs.Save();
    }
}

[System.Serializable]
public struct SettingsData {
    public float musicVolume;
    public float sfxVolume;

    public SettingsData(float musicVolume, float sfxVolume)
    {
        this.musicVolume = musicVolume;
        this.sfxVolume = sfxVolume;
    }
}

[System.Serializable]
public struct LevelData {
    public int levelPlaying;
    public int levelsCompleted;
    public LevelData(bool init)
    {
        levelPlaying = 1;
        levelsCompleted = 1;
    }
}

