    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyManager>().TakeDamage(10);
           
            Destroy(gameObject);
        }
    }
    
}
