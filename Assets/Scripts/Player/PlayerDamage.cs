using System;
using System.Collections;
using UnityEngine;

public class PlayerDamage : Damagables
{
    [SerializeField] float yAxisThreshold = -5;
    [SerializeField] float initialHealth = 1000;
    
    Material playerMaterial;
    bool isCooling;

    public Action<float> OnHealthUpdate;

    Transform m_Transform;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        playerMaterial = spriteRenderer.material;
        playerMaterial.SetInt("_Flicker", 0);

        m_Transform = transform;
    }
    private void Update()
    {
        Vector3 myPos = m_Transform.position;
        if (myPos.y < yAxisThreshold) AddDamage((int)initialHealth + 1);
    }

    public override void AddDamage(int damage)
    {
        if (isCooling) return;

        base.AddDamage(damage);
        if(OnHealthUpdate != null) OnHealthUpdate(GetCurrentHealthNormalized());

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

        GameManager.Instance.GameplayMode?.OnLoss();

        Destroy(gameObject, 2);
    }
    protected override void ResetHealth()
    {
        base.health = (int) initialHealth;
    }
    public float GetCurrentHealthNormalized()
    {
        return (health / initialHealth);
    }
}
