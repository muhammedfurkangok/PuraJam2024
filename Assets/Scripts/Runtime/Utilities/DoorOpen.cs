using DG.Tweening;
using System;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Transform door;
    private DoorInteractable doorInteractable;

    public event Action OnDoorOpened;
    public event Action OnDoorFail;

    public void InitializeDoor(DoorInteractable doorInteractable)
    {
        this.doorInteractable = doorInteractable;
    }

    public void TryOpenDoor()
    {
        if(doorInteractable.isDeviceInstalled)
        {
            OpenDoor();
            OnDoorOpened?.Invoke();
        }
        else
        {
            OnDoorFail?.Invoke();
        }
    }

    private void OpenDoor()
    {
        door.DOMoveY(door.position.y + 3f, 3f);
    }
}
