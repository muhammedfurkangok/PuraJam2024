using Cinemachine;
using Mirror;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers
{
    public class CameraController : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        [Header("Limits")]
        [SerializeField] private float minY = -90f;
        [SerializeField] private float maxY = 90f;

        private float rotationY;

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
        }
    }
}