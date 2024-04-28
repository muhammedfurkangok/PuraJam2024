using Cinemachine;
using Mirror;
using Runtime.Abstracts;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers
{
    public class PlayerInteractController : NetworkBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cam;
        [SerializeField] private float interactDistance = 5f;
        [SerializeField] private LayerMask interactLayerMask;
        
        [SerializeField] private PlayerUIController playerUIController;

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (!NetworkServer.activeHost) enabled = false;
        }

        private void Update()
        {
            if (PauseMenuManager.Instance.isGamePaused) return;

            playerUIController.UpdateUI("");
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
            
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit, interactDistance, interactLayerMask))
            {
                if(hit.collider.TryGetComponent<Interactable>(out Interactable interactable))
                {
                   playerUIController.UpdateUI(interactable.promptMessage);
                   if(Input.GetKeyDown(KeyCode.E))
                   {
                       interactable.BaseInteract();
                   }
                }
            }
                
            
        }
    }
}