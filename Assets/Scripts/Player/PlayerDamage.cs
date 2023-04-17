using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : Damagables
{
    private int initialHealth = 1000;

    Material playerMaterial;
    bool isCooling;

    private void OnEnable()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        playerMaterial = spriteRenderer.material;
        playerMaterial.SetInt("_Flicker", 0);
    }

    public override void AddDamage(int damage)
    {
        if (isCooling) return;

        base.AddDamage(damage);
        StartCoroutine(TriggerFlickeringEffect());
    }

    IEnumerator TriggerFlickeringEffect()
    {
        playerMaterial?.SetInt("_Flicker", 1);
        isCooling = true;
        yield return new WaitForSeconds(1);
        isCooling = false;
        playerMaterial?.SetInt("_Flicker", 0);
    }

    protected override void OnKill()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Kill");
        
        Rigidbody2D rBody = GetComponent<Rigidbody2D>();
        rBody.isKinematic = true;
        rBody.velocity = Vector3.zero;

        PlayerAnimationController animationController = GetComponent<PlayerAnimationController>();
        animationController.enabled = false;

        CharacterMovement characterMovement = GetComponent<CharacterMovement>();
        characterMovement.enabled = false;

        Destroy(gameObject, 2);
    }

    protected override void ResetHealth()
    {
        base.health = initialHealth;
    }
    
}
