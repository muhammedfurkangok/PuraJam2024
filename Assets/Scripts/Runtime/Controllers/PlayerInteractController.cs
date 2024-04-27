using System;
using Cinemachine;
using UnityEngine;

namespace Runtime.Controllers
{
    public class PlayerInteractController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cam;
        [SerializeField] private float interactDistance = 5f;
        [SerializeField] private LayerMask interactLayerMask;
        
        [SerializeField] private PlayerUIController playerUIController;

        private void Update()
        {
            playerUIController.UpdateUI("");
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
            
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit, interactDistance, interactLayerMask))
            {
                if(hit.collider.GetComponent<Interactable>() != null)
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
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