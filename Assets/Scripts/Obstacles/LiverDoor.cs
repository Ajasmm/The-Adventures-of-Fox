using UnityEngine;

public class LiverDoor : MonoBehaviour, IInteractable
{
    [SerializeField] Animator door;
    

    DoorState doorState = DoorState.Close;

    int doorOpen_Hash, doorClose_Hash;

    private void Awake()
    {
        doorOpen_Hash = Animator.StringToHash("Open");
        doorClose_Hash = Animator.StringToHash("Close");
    }

    public void Interact()
    {
        switch (doorState)
        {
            case DoorState.Close:
                doorState = DoorState.Open;
                door.SetTrigger(doorOpen_Hash);
                break;
            case DoorState.Open:
                doorState = DoorState.Close;
                door.SetTrigger(doorClose_Hash);
                break;
        }
    }

    private enum DoorState
    {
        Close,
        Open
    }
}
