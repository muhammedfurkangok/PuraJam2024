using Cinemachine;
using Mirror;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManager : NetworkBehaviour
    {
        [Header("References")] [SerializeField] private CinemachineVirtualCamera[] cameras;

        [Header("Info - No Touch")]
        public CinemachineVirtualCamera activeCamera;
        [SerializeField] private int index;

        public static CameraManager Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
        
        private void Start()
        {
            for (var i = 0; i < cameras.Length; i++) cameras[i].Priority = i == 0 ? 1 : 0;

            activeCamera = cameras[index];
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (!NetworkServer.activeHost) enabled = false;
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