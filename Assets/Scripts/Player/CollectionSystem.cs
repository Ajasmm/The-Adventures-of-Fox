using System.Collections.Generic;
using UnityEngine;

public class CollectionSystem : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioControler audioControl;

    Dictionary<CollectableType, int> inventory = new Dictionary<CollectableType, int>();

    private void OnEnable()
    {
        inventory.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectables collectable = collision.gameObject.GetComponent<Collectables>();
        if (collectable == null) return;

        collectable.Collect();
        if (!inventory.ContainsKey(collectable.type)) inventory.Add(collectable.type, 0);
        inventory[collectable.type]++;
        Inventory.Instance.AddItem(collectable.type, 1);
    }

    public void ResetInventoryWithMainSystem()
    {
        foreach (CollectableType type in inventory.Keys)
            Inventory.Instance.RemoveItem(type, inventory[type]);

        inventory.Clear();
    }
    public void ResetInventory()
    {
        inventory.Clear();
    }
    public void RemoveItem(CollectableType type, int count)
    {
        if (!inventory.ContainsKey(type)) return;

        if (inventory[type] >= count)
        {
            int stock = inventory[type];
            stock -= count;
            inventory[type] = stock;
        }
        else inventory[type] = 0;
    }
}
