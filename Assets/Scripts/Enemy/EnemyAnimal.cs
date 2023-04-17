using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]
public class EnemyAnimal : Damagables
{
    [SerializeField] int damage = 250;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damagables damagables = collision.gameObject.GetComponent<Damagables>();
        if (damagables == null) return;

        damagables.AddDamage(damage);
    }

    protected override void OnKill()
    {
        animator.SetTrigger("Kill");
        Destroy(this.gameObject, 1F);
        this.enabled = false;
    }

    protected override void ResetHealth()
    {
        health = 500;
    }
}
