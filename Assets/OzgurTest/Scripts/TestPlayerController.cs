using UnityEngine;

namespace OzgurTest.Scripts
{
    public class TestPlayerController : MonoBehaviour
    {
        public float speed = 5f;

        void Update()
        {
            if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * (speed * Time.deltaTime));
            if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * (speed * Time.deltaTime));
            if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * (speed * Time.deltaTime));
            if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }
    }
}
