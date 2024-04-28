using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class DoorInteractable : Interactable
{
    DoorOpen doorOpen;

    public event Action OnInteracted;
    public bool isDeviceInstalled { get; private set; } = false;    

    [SerializeField] float interactDuration = 3f;

    private void Start()
    {
        doorOpen = GetComponent<DoorOpen>();
        doorOpen.InitializeDoor(this);
    }
    protected async override void Interact()
    {
        //base.Interact();
        OnInteracted?.Invoke();
        await UniTask.WaitForSeconds(interactDuration);
        isDeviceInstalled = true;
    }
}