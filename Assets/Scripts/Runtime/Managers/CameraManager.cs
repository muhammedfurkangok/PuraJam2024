using Cinemachine;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class CameraManager : NetworkBehaviour
    {
        [Header("References")]
        public Canvas crosshairCanvas;
        public Image crosshair;
        public TextMeshProUGUI keyText;
        [SerializeField] private CinemachineVirtualCamera playerCamera;
        [SerializeField] private CinemachineVirtualCamera[] cameras;

        [Header("Info - No Touch")]
        public CinemachineVirtualCamera activeCamera;
        [SerializeField] private int index;

        public static CameraManager Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            for (var i = 0; i < cameras.Length; i++) cameras[i].Priority = i == 0 ? 1 : 0;
            activeCamera = cameras[index];

            if (NetworkServer.activeHost)
            {
                enabled = false;
                crosshairCanvas.gameObject.SetActive(false);
            }

            else playerCamera.Priority = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) SetNextCamera();
            else if (Input.GetKeyDown(KeyCode.E)) SetPreviousCamera();
        }

        private void SetNextCamera()
        {
            activeCamera.Priority = 0;
            index = index == cameras.Length - 1 ? 0 : index + 1;
            activeCamera = cameras[index];
            activeCamera.Priority = 1;
        }

        private void SetPreviousCamera()
        {
            activeCamera.Priority = 0;
            index = index == 0 ? cameras.Length - 1 : index - 1;
            activeCamera = cameras[index];
            activeCamera.Priority = 1;
        }
    }
}