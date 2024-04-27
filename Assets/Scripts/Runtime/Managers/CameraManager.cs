using Cinemachine;
using Mirror;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManager : NetworkBehaviour
    {
        [Header("References")] [SerializeField] private CinemachineVirtualCamera[] cameras;
        [Header("Info - No Touch")] [SerializeField] private CinemachineVirtualCamera activeCamera;

        public static CameraManager Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
        
        private void Start()
        {
            for (var i = 0; i < cameras.Length; i++) cameras[i].Priority = i == 0 ? 1 : 0;
            activeCamera = cameras[0];
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (NetworkServer.activeHost)
            {
                SceneLog.Instance.Log("Disabling CameraManager.");
                enabled = false;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) SetActiveCamera(activeCamera == cameras[0] ? 1 : 0);
        }

        public void SetActiveCamera(int index)
        {
            activeCamera.Priority = 0;
            activeCamera = cameras[index];
            activeCamera.Priority = 1;
        }
    }
}