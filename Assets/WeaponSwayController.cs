using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwayController : MonoBehaviour
{

    [SerializeField] private float intensity;
    [SerializeField] private float smooth;
    
    private float InputX, InputY;
    private Quaternion originRotation;
    // Start is called before the first frame update
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
}
