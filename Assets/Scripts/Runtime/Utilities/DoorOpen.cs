using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] Transform door;
    [SerializeField] bool easyUnlock;
    DoorInteractable doorInteractable;


    public event Action OnDoorOpened;
    public event Action OnDoorFail;

#if UNITY_EDITOR
    async void Start()
    {
        await UniTask.WaitForSeconds(4f);
        OpenDoor();
    }

#endif

    public void InitializeDoor(DoorInteractable doorInteractable)
    {
        this.doorInteractable = doorInteractable;
    }

    public void TryOpenDoor()
    {
        if(doorInteractable.isDeviceInstalled || easyUnlock)
        {
            OpenDoor();
            OnDoorOpened?.Invoke();
        }
        else
        {
            OnDoorFail?.Invoke();
        }
    }

    void OpenDoor()
    {
       door.DOMoveY(door.position.y + 3f, 3f);
    }
}
