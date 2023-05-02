using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]
[ExecuteAlways]
public class EnemyAnimal : Damagables
{
    [SerializeField] int damage = 250;
    [SerializeField] Collider2D enemyCollider;
    [SerializeField] AudioSource destroySound;

    Transform myTransform;
    Animator animator;

    private void Start()
    {
        myTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damagables damagables = collision.gameObject.GetComponent<Damagables>();
        if (damagables == null) return;

        Vector3 direction = myTransform.InverseTransformPoint(collision.contacts[0].point);
        direction.Normalize();

        damagables.AddDamage(damage);
        damagables.AddForce(direction * 50F);
    }

    protected override void OnKill()
    {
        animator.SetTrigger("Kill");
        if(enemyCollider) enemyCollider.enabled = false;
        destroySound.Play();

        Destroy(this.gameObject, 1F);
        this.enabled = false;
    }

    protected override void ResetHealth()
    {
        health = 500;
    }
}
