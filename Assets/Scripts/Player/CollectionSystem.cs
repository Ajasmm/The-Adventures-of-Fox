using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class CollectionSystem : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioControler audioControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectables collectable = collision.gameObject.GetComponent<Collectables>();
        if (collectable == null) return;

        collectable.Collect();
        Inventory.Instance.AddItem(collectable.type);

        switch (collectable.type)
        {
            case CollectableType.Cherry:
                audioControl.Cherry();
                break;
            case CollectableType.Gems:
                audioControl.Gem();
                break;
        }

    }
}
