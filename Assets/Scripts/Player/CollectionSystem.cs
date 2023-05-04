using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSystem : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioControler audioControl;

    Dictionary<CollectableType, int> inventory = new Dictionary<CollectableType, int>();

    public OnItemCollect OnItemCollected;

    private void OnEnable()
    {
        inventory.Clear();
        SyncWithInventory();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectables collectable = collision.gameObject.GetComponent<Collectables>();
        if (collectable == null) return;

        collectable.Collect();
        AddItem(collectable.type, 1);

        if (OnItemCollected != null) OnItemCollected(collectable.type, inventory[collectable.type]);
    }
    
    public int GetItemCount(CollectableType type)
    {
        if (inventory.ContainsKey(type)) return inventory[type];
        else return 0;
    }

    public void SyncWithInventory()
    {
        foreach(InventoryItem item in Inventory.Instance.GetList())
        {
            if (!inventory.ContainsKey(item.type)) inventory.Add(item.type, 0);
            inventory[item.type] = item.count;
            if (OnItemCollected != null) OnItemCollected(item.type, inventory[item.type]);
        }
    }
    public void UpdateMainInventory()
    {
        foreach (CollectableType type in inventory.Keys)
            Inventory.Instance.UpdateItem(type, inventory[type]);


    }
    public bool GetItem(CollectableType type, int count)
    {
        if (!inventory.ContainsKey(type)) return false;
        if (inventory[type] < count) return false;

        inventory[type] -= count;
        if(OnItemCollected != null) OnItemCollected(type, inventory[type]);
        return true;
    }
    public void AddItem(CollectableType type, int count)
    {
        if (!inventory.ContainsKey(type)) inventory.Add(type, 0);
        inventory[type] += count;
        if (OnItemCollected != null) OnItemCollected(type, inventory[type]);
    }

}

public delegate void OnItemCollect(CollectableType type, int count);
