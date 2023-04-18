using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]
public class ThrowableCherry : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] int damage = 500;

    Vector3 velocity;
    Vector3 directon = Vector3.right;

    Rigidbody2D rBody;
    ThrowSystem throwSystem;
    Transform m_Transform;
    Transform parent;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.bodyType = RigidbodyType2D.Kinematic;
        rBody.useFullKinematicContacts = true;
        rBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
    private void FixedUpdate()
    {
        velocity = directon * speed;
        rBody.velocity = velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damagables damagable = collision.gameObject.GetComponent<Damagables>();
        if (damagable == null)
        {
            throwSystem.AddtoPool(this);
            return;
        }

        damagable.AddDamage(damage);
        throwSystem.AddtoPool(this);
    }

    public void InitializeThrowable(ThrowSystem throwSystem, Transform parent)
    {
        m_Transform = transform;

        this.throwSystem = throwSystem;
        this.parent = parent;
    }
    public void Enable(Vector3 pos, Vector3 direction)
    {
        m_Transform.parent = null;
        m_Transform.position = pos;
        Debug.Log(pos);

        this.directon = direction;
        this.gameObject.SetActive(true);
    }
    public void Disable()
    {
        m_Transform.parent = parent;
        gameObject.SetActive(false);
    }

}
