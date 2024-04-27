using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health = 100f;
    public float damage = 10f;
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <= 0)
        {
            Die();
        }
       
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
