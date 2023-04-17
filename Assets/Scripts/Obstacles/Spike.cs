using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] int damage = 250;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagables damagables = collision.gameObject.GetComponent<Damagables>();
        if (damagables == null ) return;

        damagables.AddDamage(damage);
    }
}
