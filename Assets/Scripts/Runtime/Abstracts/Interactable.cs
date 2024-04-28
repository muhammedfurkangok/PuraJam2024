using Mirror;

namespace Runtime.Abstracts
{
    public abstract class Interactable : NetworkBehaviour
    {
        //player objeye baktiginda cikacak mesaj
        public string promptMessage;

        //bu fonksiyon cagirilacak
        public void BaseInteract()
        {
            Interact();
        }

        protected virtual void Interact()
        {
            //template
        }
    }
}
