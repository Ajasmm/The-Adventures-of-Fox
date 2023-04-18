using UnityEngine;

public abstract class Collectables : MonoBehaviour
{
    public CollectableType type; 

    public virtual void Collect()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        Destroy(this.gameObject, 35/60F);
    }
}
