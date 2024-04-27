using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    public float speed = 90f;

    void Update()
    {
        if (Input.GetKey(KeyCode.D)) transform.Rotate(Vector3.up, speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A)) transform.Rotate(Vector3.down, speed * Time.deltaTime);
    }
}
