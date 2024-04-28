using DG.Tweening;
using Mirror;

namespace Runtime.Utilities
{
    public class DoorOpen : NetworkBehaviour
    {
        public void OpenDoor()
        {
            OpenDoorCommand();
        }

        [Command(requiresAuthority = false)]
        private void OpenDoorCommand()
        {
            print("open door command called");
            transform.DOMoveY(transform.position.y + 3f, 3f);
        }
    }
}
