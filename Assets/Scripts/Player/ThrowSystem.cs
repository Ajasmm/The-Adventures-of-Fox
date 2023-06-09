using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowSystem : MonoBehaviour
{
    [Header("Throw System")]
    [SerializeField] CollectableType type;
    [SerializeField] ThrowableCherry trowableObject;
    [SerializeField] CollectionSystem collectionSystem;

    [Header("Audio")]
    [SerializeField] private AudioControler audioControl;

    int poolSize = 10;
    Vector3 throwOffset = new Vector3(0, 1, 0);

    Transform poolObjectParent;
    Transform m_Transform;
    MyInput input;

    Queue<ThrowableCherry> throwablePool;

    private void Start()
    {
        m_Transform = transform;
        InitializePool();
    }
    private void OnEnable()
    {
        input = GameManager.Instance.input;
        input.GamePlay.Throw.performed += Throw;
    }
    private void OnDisable()
    {
        input.GamePlay.Throw.performed -= Throw;
    }

    private void Throw(InputAction.CallbackContext context)
    {
        if (collectionSystem.GetItem(type, 1))
        {
            ThrowableCherry throwable = GetObject();
            if (throwable == null)
            {
                collectionSystem.AddItem(type, 1);
                return;
            }

            audioControl.Throw();
            throwable.Enable(m_Transform.position + throwOffset, Vector3.right * m_Transform.localScale.x);
        }
    }

    private void InitializePool()
    {
        throwablePool = new Queue<ThrowableCherry>();

        GameObject poolObject = new GameObject("Pool Object Parent");
        poolObjectParent = poolObject.transform;
        poolObjectParent.parent = m_Transform;
        poolObjectParent.localPosition = Vector3.zero;

        for(int i = 0; i < poolSize; i++)
        {
            ThrowableCherry cherry = Instantiate(trowableObject, poolObjectParent);
            cherry.InitializeThrowable(this, m_Transform);
            AddtoPool(cherry);
        }
    }

    public void AddtoPool(ThrowableCherry throwableObject)
    {
        throwableObject.Disable();
        throwablePool.Enqueue(throwableObject);
    }
    public ThrowableCherry GetObject()
    {
        if (throwablePool.Count <= 0) return null;
        return throwablePool.Dequeue();
    }
}
