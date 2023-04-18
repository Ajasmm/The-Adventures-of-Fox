using UnityEngine;

public abstract class Damagables : MonoBehaviour
{
    public int Health { get { return health; } }
    protected int health = 1000;

    private void OnEnable()
    {
        ResetHealth();
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

    protected abstract void OnKill();
    protected abstract void ResetHealth();
}
