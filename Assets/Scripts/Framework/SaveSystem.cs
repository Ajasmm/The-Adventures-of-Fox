using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveSystem
{
    static string settingsKey = "SettingsData";
    static string levelKey = "LevelData";
    static string inventoryKey = "Inventory";

    static string jsonData;

    static SettingsData settingsData;
    static LevelData levelData;
    static InventoryData inventoryData;

    public static void SyncSettingsData()
    {
        if (PlayerPrefs.HasKey(settingsKey))
        {
            jsonData = PlayerPrefs.GetString(settingsKey);
            settingsData = JsonUtility.FromJson<SettingsData>(jsonData);
        }
        else settingsData = new SettingsData(1, 1);
        
        AudioManager.Instance.SetVolume(AudioChannels.MASTER, 1);
        AudioManager.Instance.SetVolume(AudioChannels.MUSIC, settingsData.musicVolume);
        AudioManager.Instance.SetVolume(AudioChannels.SFX, settingsData.sfxVolume);
    }
    public static void SyncLevelData()
    {
        if (PlayerPrefs.HasKey(levelKey))
        {
            jsonData = PlayerPrefs.GetString(levelKey);
            levelData = JsonUtility.FromJson<LevelData>(jsonData);
        }

        if (levelData.levelPlaying < 1) levelData.levelPlaying = 1;
        if(levelData.levelsCompleted < 1) levelData.levelsCompleted = 1;

        GameManager.Instance.levelData = levelData;
    }
    public static void SyncInventoryData()
    {
        if (PlayerPrefs.HasKey(inventoryKey))
        {
            jsonData = PlayerPrefs.GetString(inventoryKey);
            inventoryData = JsonUtility.FromJson<InventoryData>(jsonData);

            if (inventoryData.list == null) inventoryData.list = new List<InventoryItem>();

            foreach(InventoryItem inventoryItem in inventoryData.list) 
                Inventory.Instance.AddItem(inventoryItem.type, inventoryItem.count);

            Debug.Log("Save data : " + jsonData);
        }
        if (inventoryData.list == null) inventoryData = new InventoryData(null);

    }

    public static void SaveSettingsData()
    {
        settingsData.musicVolume = AudioManager.Instance.musicVolume;
        settingsData.sfxVolume = AudioManager.Instance.sfxVolume;

        jsonData = JsonUtility.ToJson(settingsData);
        PlayerPrefs.SetString(settingsKey, jsonData);
        PlayerPrefs.Save();
    }
    public static void SaveLevelData()
    {
        levelData = GameManager.Instance.levelData;

        jsonData = JsonUtility.ToJson(levelData);
        PlayerPrefs.SetString(levelKey, jsonData);
        PlayerPrefs.Save();
    }
    public static void SaveInventoryData()
    {
        inventoryData.list = Inventory.Instance.GetList();

        jsonData = JsonUtility.ToJson(inventoryData);
        PlayerPrefs.SetString(inventoryKey, jsonData);
        PlayerPrefs.Save();


        Debug.Log("Save data : " + jsonData);
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

[System.Serializable]
public struct InventoryData
{
    [Serialize] public List<InventoryItem> list;

    public InventoryData(Dictionary<CollectableType, int> inventory)
    {
        list = new List<InventoryItem>();
        
        if (inventory == null) return;

        foreach(CollectableType type in inventory.Keys)
            list.Add(new InventoryItem(type, inventory[type]));
    }
}

[Serializable]
public struct InventoryItem
{
    public CollectableType type;
    public int count;

    public InventoryItem(CollectableType type, int count)
    {
        this.type = type;
        this.count = count;
    }
}

