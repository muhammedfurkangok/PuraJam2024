using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
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

    private const float _walkSpeed = 2f;
    private const float _runSpeed = 6f;

    private Vector3 _velocity;

    private bool isGrounded;
    private bool isRunning;
    private bool isMoving;

    private void Start()
    {
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
}
