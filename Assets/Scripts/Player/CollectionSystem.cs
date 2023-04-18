using UnityEngine;

public class CollectionSystem : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectables collectable = collision.gameObject.GetComponent<Collectables>();
        if (collectable == null) return;

        collectable.Collect();
        Inventory.Instance.AddItem(collectable.type);
    }
}
