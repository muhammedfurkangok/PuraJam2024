using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    
    [SerializeField] private Transform Camera;
    [SerializeField] private Transform CameraRoot;
    [SerializeField] private float MouseSensitivity = 100f;
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

    private bool _hasAnimator;
    private int _xVelocityHash;
    private int _zVelocityHash;
    private float _xRotation ;

    private const float _walkSpeed = 2f;
    private const float _runSpeed = 6f;

    private Vector3 _velocity;

    private bool isGrounded;
    private bool isRunning;
    private bool isMoving;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _hasAnimator = animator != null;
        Debug.Log(animator);
        if (_hasAnimator)
        {
            _xVelocityHash = Animator.StringToHash("X_Velocity");
            _zVelocityHash = Animator.StringToHash("Y_Velocity");
        }
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
            isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) 
            isRunning = false;
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        isMoving = Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f;
        
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        Move();
        CamMovements();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        _velocity.y += gravity * Time.deltaTime;
        
    }

    private void Move()
    {
        if (!_hasAnimator) return;
      
        float targetSpeed = isRunning ? _runSpeed : _walkSpeed;
        if (!isMoving) targetSpeed = 0.1f;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 targetVelocity = (transform.right * horizontalInput + transform.forward * verticalInput) * targetSpeed;
        
        
        _velocity =  Vector3.Lerp(_velocity, targetVelocity, AnimBlendSpeed * Time.fixedDeltaTime); 

        animator.SetFloat(_xVelocityHash, _velocity.x);
        animator.SetFloat(_zVelocityHash, _velocity.z);

        playerRigidbody.velocity = _velocity;
    }
    private void CamMovements()
    {
        if(!_hasAnimator) return;

        var Mouse_X =Input.GetAxis("Mouse X");
        var Mouse_Y = Input.GetAxis("Mouse Y") ;
        // Camera.position = CameraRoot.position;
       
            
        _xRotation -= Mouse_Y * MouseSensitivity * Time.smoothDeltaTime;
        _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

        eyes.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        //arms.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        // Camera.localRotation = Quaternion.Euler(_xRotation, 0 , 0);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(0, Mouse_X * MouseSensitivity * Time.smoothDeltaTime, 0));
    }

}
