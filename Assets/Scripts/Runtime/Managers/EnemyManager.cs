using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health = 100f;
    public float damage = 10f;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private ZombieManager zombieManager;
    
    const string death = "death";
    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <= 0)
        {
            Die();
            zombieManager.enabled = false;
            this.enabled = false;
        }
       
    }

    [Button]
    public void Die()
    {
        enemyAnimator.Play(death);
        Destroy(gameObject,3f);
    }
}
