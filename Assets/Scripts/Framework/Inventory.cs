using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance
    {
        get { return GetInstance(); }
        private set { instance = value; }
    }
    private static Inventory instance;

    private Dictionary<CollectableType, int> m_Inventory;

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

        m_Inventory = new Dictionary<CollectableType, int>();
    }
    private void OnDestroy()
    {
        isGameFinished = true;
    }

    public void AddItem(CollectableType type)
    {
        int stock;
        m_Inventory.TryGetValue(type, out stock);

        stock++;
        m_Inventory[type] = stock;
        TriggerUIEvent(type, stock);
    }
    public bool GetItem(CollectableType type)
    {
        int stock;
        m_Inventory.TryGetValue(type, out stock);
        
        if(stock > 0)
        {
            stock--;
            m_Inventory[type] = stock;
            TriggerUIEvent(type, stock);
            return true;
        }
        return false;
    }
    public void GetItemCount(CollectableType type, out int count)
    {
        m_Inventory.TryGetValue(type, out  count);
    }

    private void TriggerUIEvent(CollectableType type, int count)
    {
        switch (type)
        {
            case CollectableType.Gems:
                if(OnGemUpdate != null) OnGemUpdate(count);
                break;
            case CollectableType.Cherry:
                if(OnCherryUpdate != null) OnCherryUpdate(count);
                break;
        }
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
