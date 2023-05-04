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
    private void Start()
    {
        // Updating the throw button if the cherry count is changed
        // also adding cherry to the inventory if there is no cherry
        if (!inventory.ContainsKey(CollectableType.Cherry)) inventory.Add(CollectableType.Cherry, 0);
        GameManager.Instance.androidController?.SetThrowButton(inventory[CollectableType.Cherry] > 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectables collectable = collision.gameObject.GetComponent<Collectables>();
        if (collectable == null) return;

        collectable.Collect();
        AddItem(collectable.type, 1);

        InvokeUpdate(collectable.type, inventory[collectable.type]);
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
            InvokeUpdate(item.type, inventory[item.type]);
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
        InvokeUpdate(type, inventory[type]);
        return true;
    }
    public void AddItem(CollectableType type, int count)
    {
        if (!inventory.ContainsKey(type)) inventory.Add(type, 0);
        inventory[type] += count;
        InvokeUpdate(type, inventory[type]);
    }
    private void InvokeUpdate(CollectableType type, int count)
    {
        if (OnItemCollected != null) OnItemCollected(type, count);

        // Updating the throw button if the cherry count is changed
        // also adding cherry to the inventory if there is no cherry
        if(!inventory.ContainsKey(CollectableType.Cherry)) inventory.Add(CollectableType.Cherry, 0);
        if(type == CollectableType.Cherry) GameManager.Instance.androidController?.SetThrowButton(inventory[CollectableType.Cherry] > 0);
    }

}

public delegate void OnItemCollect(CollectableType type, int count);
