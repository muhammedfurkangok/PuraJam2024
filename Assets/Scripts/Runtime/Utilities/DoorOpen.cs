using DG.Tweening;
using Mirror;

namespace Runtime.Utilities
{
    public class DoorOpen : NetworkBehaviour
    {
        [Command(requiresAuthority = false)]
        private void OpenDoor()
        {
            transform.DOMoveY(transform.position.y + 3f, 3f);
        }
    }
}
