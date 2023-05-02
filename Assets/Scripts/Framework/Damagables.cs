using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Damagables : MonoBehaviour
{
    public int Health { get { return health; } }
    [SerializeField] Rigidbody2D rBody;

    protected int health = 1000;

    protected void OnEnable()
    {
        ResetHealth();
    }
    private void Start()
    {
        if(rBody == null) rBody = GetComponent<Rigidbody2D>();
    }
    public virtual void AddDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            OnKill();
        }
    }
    public virtual void AddForce(Vector3 force)
    {
        if (rBody == null) return;

        rBody.AddForce(force,ForceMode2D.Impulse);
    }

    protected abstract void OnKill();
    protected abstract void ResetHealth();
}
