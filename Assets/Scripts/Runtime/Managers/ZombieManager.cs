using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ZombieManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public NavMeshAgent enemyNavMeshAgent;
    public Transform player;
    public LayerMask whatIsPlayer;

    public float sightRange, attackRange, alertedSightRange, alertedAttackRange;
    public bool playerInSightRange, playerInAttackRange, playerInAttackRangeAlerted, playerInSightRangeAlerted, isAlerted;

    #endregion

    #region Serialized Variables
    
    [SerializeField] public Animator enemyAnimator;
    [SerializeField] private GameObject alertPlace;

    #endregion

    #region Constants
    string currentAnimation;
    const string idle1 = "idle1";
    const string idle2 = "idle2";
    const string walk1 = "walk1";
    const string run1 = "run1";
    const string run2 = "run2";
    const string attack1 = "attack1";
    const string attack2 = "attack2";
    const string die1 = "die1";
    const string die2 = "die2";
      
    private bool isPatrolingStarted;
    private bool isPatrolingFinished;
    private bool isChasing;
    private bool isAttacking;

    #endregion
    #region Private Variables
    
    private Vector3 nextPosition;
    
    #endregion

    #endregion
    
    private void Start()
    {
        enemyAnimator.Play(walk1);
        ZombieSignals.Instance.OnZombiesAlerted += Alerted;
       
    }

    private void Alerted()
    {
        isAlerted = true;
    }
    private void OnDisable()
    {
        ZombieSignals.Instance.OnZombiesAlerted -= Alerted;
        
    }

    private void Update()
    { 
       
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInAttackRangeAlerted = Physics.CheckSphere(transform.position, alertedAttackRange, whatIsPlayer);
        playerInSightRangeAlerted = Physics.CheckSphere(transform.position, alertedSightRange, whatIsPlayer);
        
        if (!isAlerted)
        {
            if (!playerInSightRange && !playerInAttackRange) 
                Patroling();
            else if (playerInSightRange && !playerInAttackRange) 
                ChasePlayer();
            else if (playerInSightRange && playerInAttackRange) 
                AttackPlayer();
        }
        else // If alerted
        {
            if (!playerInSightRange) 
                GoToAlertPlace();
            else if (playerInSightRangeAlerted) 
                ChasePlayer();
            else if (playerInAttackRangeAlerted) 
                AttackPlayer();
        }
    }

    [Button]
    private void GoToAlertPlace()
    {
        ChangeAnimation(walk1);
        enemyNavMeshAgent.SetDestination(alertPlace.transform.position);
    }


    private async void AttackPlayer()
    {
        if(isAttacking) return;
        
        isAttacking = true;
        
        string attackAnimation = Random.Range(0, 2) == 0 ? attack1 : attack2;
        transform.LookAt(player);
        
        ChangeAnimation(attackAnimation);
        //wait until animation end
        await UniTask.WaitForSeconds(attackAnimation == attack1 ? 2.63f : 4.17f);
        isAttacking = false;
    }
    


    private void ChasePlayer()
    {
        if(isChasing) return;
        
        isChasing = true;
        string runAnimation = Random.Range(0, 2) == 0 ? run1 : run2;
        ChangeAnimation(runAnimation);
        
        enemyNavMeshAgent.SetDestination(player.position);
     
    }

    private async void Patroling()
    {
        if(isPatrolingStarted) return;
        
        nextPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        isPatrolingStarted = true;
        isPatrolingFinished = false;
        
        enemyNavMeshAgent.SetDestination(nextPosition);
        ChangeAnimation(walk1);

        await UniTask.WaitUntil(() => enemyNavMeshAgent.remainingDistance <= 0.1f);
        
        isPatrolingFinished = true;

        if (isPatrolingFinished)
        {
            string idleAnimation = Random.Range(0, 2) == 0 ? idle1 : idle2;
            ChangeAnimation(idleAnimation);
            await UniTask.WaitForSeconds(5);
            isPatrolingStarted = false;
            isPatrolingFinished = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, alertedAttackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertedSightRange);
    }
    private void ChangeAnimation(string animation)
    {
        if(currentAnimation == animation) return;
        
        enemyAnimator.Play(animation);
        currentAnimation = animation;
    }
}
