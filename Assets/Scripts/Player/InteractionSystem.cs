using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionSystem : MonoBehaviour
{
    HashSet<IInteractable> interactables;

    MyInput input;

    IInteractable interactable;
    
    private void Awake()
    {
        interactables = new HashSet<IInteractable>();
    }
    private void OnEnable()
    {
        input = GameManager.Instance.input;

        input.GamePlay.Interact.performed += Interact;
    }
    private void OnDisable()
    {
        input.GamePlay.Interact.performed -= Interact;
    }
    private void Start()
    {
        UpdateController();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        interactable?.Interact();
    }
    private void UpdateController()
    {
        GameManager.Instance.androidController?.SetInteractionButton(interactables.Count > 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.gameObject.GetComponentInParent<IInteractable>();
        if (interactable != null) interactables.Add(interactable);
         
        this.interactable = interactable;
        UpdateController();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.gameObject.GetComponentInParent<IInteractable>();
        if (interactable != null) interactables.Remove(interactable);

        if (interactable == this.interactable)
        {
            this.interactable = null;
            foreach(IInteractable val in interactables)
            {
                if (val != null)
                {
                    this.interactable = val;
                    break;
                }
            }
        }

        UpdateController();
    }
}
