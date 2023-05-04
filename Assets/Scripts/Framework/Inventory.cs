using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance
    {
        get { return GetInstance(); }
        private set { instance = value; }
    }
    private static Inventory instance;

    private Dictionary<CollectableType, int> m_Inventory = new Dictionary<CollectableType, int>();

    public Action<int> OnGemUpdate;
    public Action<int> OnCherryUpdate;

    private static bool isGameFinished = false;
    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(instance != this) 
            Destroy(gameObject);

        SaveSystem.SyncInventoryData();

    }
    private void OnDestroy()
    {
        if (instance != this) return;

        Save();
        isGameFinished = true;
    }

   
    public void UpdateItem(CollectableType type, int count)
    {
        if(!m_Inventory.ContainsKey(type)) m_Inventory.Add(type, 0);
        m_Inventory[type] = count;
    }
    public void Save()
    {
        SaveSystem.SaveInventoryData();
    }

    public List<InventoryItem> GetList()
    {
        List<InventoryItem> list = new List<InventoryItem>();
        if (m_Inventory == null)
        {
            Debug.Log("List is empty");
            return list;
        }

        foreach (CollectableType type in m_Inventory.Keys)
            list.Add(new InventoryItem(type, m_Inventory[type]));
        
        return list;
    }
    private static Inventory GetInstance()
    {
        if (isGameFinished) return null;

        if (instance == null)
        {
            GameObject Inventory = new GameObject("Inventory");
            Inventory.AddComponent<Inventory>();
        }
        return instance;
    }
}
