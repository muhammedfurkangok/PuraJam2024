using Runtime.Abstracts;

namespace Runtime.Utilities
{
    public class DoorInteractable : Interactable
    {
        protected override void Interact()
        {
            base.Interact();
            GetComponent<DoorOpen>().OpenDoor();
        }
    }
}