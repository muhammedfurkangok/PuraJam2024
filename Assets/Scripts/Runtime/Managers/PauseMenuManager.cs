using UnityEngine;

namespace Runtime.Managers
{
    public class PauseMenuManager : MonoBehaviour
    {
        [Header("References")] [SerializeField] private Canvas canvas;
        [Header("Info - No Touch")] public bool isGamePaused;

        public static PauseMenuManager Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            //Default settings
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused) ContinueGame();
                else PauseGame();
            }
        }

        private void ContinueGame()
        {
            isGamePaused = false;
            canvas.enabled = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void PauseGame()
        {
            isGamePaused = true;
            canvas.enabled = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
