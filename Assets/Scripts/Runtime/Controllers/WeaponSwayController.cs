using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class WeaponSwayController : MonoBehaviour
{

    [SerializeField] private float intensity;
    [SerializeField] private float smooth;
    
    private float InputX, InputY;
    private Quaternion originRotation;
    // Start is called before the first frame update


    [SerializeField] private LayerMask groundLayer;
    private float speedCurve;

    private float curveSin{get => Mathf.Sin(Time.time * speedCurve);}
    private float curveCos{get => Mathf.Cos(curveSin);}

    public Vector3 travelLimit = Vector3.one * 0.025f;
    public Vector3 bobLimit = Vector3.one * 0.1f;
    
    private Vector3 bobPosition;
    private bool isGrounded;
    void Start()
    {
        
        originRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSway();
    }

    private void CalculateSway()
    {
        InputX = -Input.GetAxis("Mouse X");
        InputY = -Input.GetAxis("Mouse Y");
    }
    private void UpdateSway()
    {
        float moveX = -Input.GetAxis("Mouse X") ;
        float moveY = -Input.GetAxis("Mouse Y") ;
    
        Quaternion t_adjX = Quaternion.AngleAxis(  -intensity * moveX, Vector3.up);
        Quaternion t_adjY = Quaternion.AngleAxis(intensity * moveY, Vector3.right);
        Quaternion target_Rotation  = originRotation * t_adjX * t_adjY;
        
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_Rotation, Time.deltaTime * smooth);
    }

    private void BobOffset()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);
        speedCurve += Time.deltaTime * (isGrounded ? 2 : 1);
        
    }
}
