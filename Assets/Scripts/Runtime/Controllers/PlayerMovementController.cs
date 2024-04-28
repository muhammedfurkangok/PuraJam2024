using Mirror;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Runtime.Controllers
{
    public class PlayerMovementController : NetworkBehaviour
    {
        [SerializeField] private Transform Camera;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float BottomLimit = 70f;
        [SerializeField] private GameObject eyes;
        [SerializeField] private GameObject arms;

        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private Animator animator;
        [SerializeField] private float speed = 12f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 3f;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;
        
        [Header("Player Health")]
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth;
        [SerializeField] private Slider healthBar;
        

        private bool _hasAnimator;
        private int _xVelocityHash;
        private int _zVelocityHash;
        private int _yRotationHash ;
        private float _xRotation ;

        private const float _walkSpeed = 2f;
        private const float _runSpeed = 6f;

        private Vector3 _velocity;

        private bool isGrounded;
        private bool isRunning;
        private bool isMoving;

        private void Start()
        {
            _xVelocityHash = Animator.StringToHash("X_Velocity");
            _zVelocityHash = Animator.StringToHash("Y_Velocity");
            _yRotationHash = Animator.StringToHash("Y_Rotation");
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!NetworkServer.activeHost) enabled = false;
        }

        private void Update()
        {
           
            if (PauseMenuManager.Instance.isGamePaused) return;

            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning = true;
            else if (Input.GetKeyUp(KeyCode.LeftShift)) isRunning = false;

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            isMoving = Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f;

            if (isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            CamMovements();
            Move();

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            _velocity.y += gravity * Time.deltaTime;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            healthBar.value = currentHealth;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
                
            }
        }

        private void Die()
        {
            SceneManager.LoadScene("DisconnectScene");
        }


        private void Move()
        {
            var targetSpeed = isRunning ? _runSpeed : _walkSpeed;
            if (!isMoving) targetSpeed = 0.1f;

            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            var targetVelocity = (transform.right * horizontalInput + transform.forward * verticalInput) * targetSpeed;

            _velocity = Vector3.Lerp(_velocity, targetVelocity, AnimBlendSpeed * Time.deltaTime);

            animator.SetFloat(_xVelocityHash, _velocity.x);
            animator.SetFloat(_zVelocityHash, _velocity.z);

            playerRigidbody.velocity = _velocity;
        }

        private void CamMovements()
        {
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");

            // Camera.position = CameraRoot.position;
            //arms.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            // Camera.localRotation = Quaternion.Euler(_xRotation, 0 , 0);

            _xRotation -= mouseY * PlayerPrefs.GetFloat("Sensitivity") / 3;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            eyes.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            //playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(0, Mouse_X * PlayerPrefs.GetFloat("Sensitivity") * Time.smoothDeltaTime, 0));
            transform.Rotate(Vector3.up * (mouseX * PlayerPrefs.GetFloat("Sensitivity") / 3));
        }
    }
}
