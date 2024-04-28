using Cinemachine;
using Mirror;
using Runtime.Abstracts;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers
{
    public class CameraController : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private LayerMask interactLayerMask;

        [Header("Limits")]
        [SerializeField] private float minY = -90f;
        [SerializeField] private float maxY = 90f;
        [SerializeField] private float minX = 5f;
        [SerializeField] private float maxX = 20f;

        private float rotationY;
        private float rotationX;

        private void Start()
        {
            rotationY = transform.eulerAngles.y;

            if (NetworkServer.activeHost) enabled = false;
        }

        private void Update()
        {
            if (PauseMenuManager.Instance.isGamePaused) return;
            if (CameraManager.Instance.activeCamera != virtualCamera) return;

            var mouseX = Input.GetAxis("Mouse X") * PlayerPrefs.GetFloat("Sensitivity") * Time.deltaTime;

            rotationY += mouseX;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            var euler = transform.eulerAngles;
            euler.y = rotationY;
            transform.eulerAngles = euler;

            var mouseY = Input.GetAxis("Mouse Y") * PlayerPrefs.GetFloat("Sensitivity") * Time.deltaTime;

            rotationX += mouseY;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);

            euler = transform.eulerAngles;
            euler.x = rotationX;
            transform.eulerAngles = euler;

            ////////////////////////////////////////////

            // Create a ray that starts from the middle of the screen
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            // Define the maximum distance the ray should check for collisions
            float maxDistance = 100f;

            // Perform the raycast
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance, interactLayerMask))
            {
                // If the ray hit something, this code will be executed
                //Debug.Log("Ray hit: " + hit.collider.gameObject.name);
                CameraManager.Instance.keyText.enabled = true;
                CameraManager.Instance.crosshair.color = Color.red;
                if (Input.GetKeyDown(KeyCode.W)) hit.collider.GetComponent<Interactable>().BaseInteract();
            }

            // If the ray didn't hit anything, this code will be executed
            else
            {
                //Debug.Log("Ray didn't hit anything");
                CameraManager.Instance.keyText.enabled = false;
                CameraManager.Instance.crosshair.color = Color.white;
            }
        }
    }
}