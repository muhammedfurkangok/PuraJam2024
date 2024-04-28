using System;

public class DoorInteractable : Interactable
{
    public bool isDeviceInstalled;
    private DoorOpen doorOpen;

    private void Start()
    {
        doorOpen = GetComponent<DoorOpen>();
        doorOpen.InitializeDoor(this);
    }

    protected override void Interact()
    {
        isDeviceInstalled = true;
    }
}