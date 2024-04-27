using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Vector3 _acceleration = new Vector3(0, 0, 0);

    private Vector3 _speed = new Vector3(0, 0, 0);
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _acceleration = new Vector3(_acceleration.x, _acceleration.y, 1);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            
        }
    }
}
