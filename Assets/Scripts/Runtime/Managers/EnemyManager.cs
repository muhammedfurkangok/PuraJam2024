using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health = 100f;
    public float damage = 10f;
    [SerializeField] private Animator enemyAnimator;
    
    const string die1 = "death1";
    const string die2 = "death2";
    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <= 0)
        {
            Die();
        }
       
    }

    [Button]
    public void Die()
    {
        string currentAnimation = Random.Range(0, 2) == 0 ? die1 : die2;
        enemyAnimator.Play(currentAnimation);
        Destroy(gameObject,3f);
    }
}
