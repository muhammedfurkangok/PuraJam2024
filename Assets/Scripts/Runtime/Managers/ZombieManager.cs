using DG.Tweening;
using Sirenix.OdinInspector;
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

    public float sightRange, attackRange, alertedSightRange,alertedAttackRange;
    public bool playerInSightRange, playerInAttackRange,playerInAttackRangeAlerted,playerInSightRangeAlerted,isAlerted;

    #endregion

    #region Serialized Variables
    
    [SerializeField] public Animator enemyAnimator;
    [SerializeField] private GameObject alertPlace;

    #endregion

    #region Private Variables
    
    private Vector3 nextPosition;
    
    #endregion

    #endregion
    
    private void Update()
    { 
       
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInAttackRangeAlerted = Physics.CheckSphere(transform.position, alertedAttackRange, whatIsPlayer);
        playerInSightRangeAlerted = Physics.CheckSphere(transform.position, alertedSightRange, whatIsPlayer);
        
             if (!playerInSightRange && !playerInAttackRange && !isAlerted) Patroling();
             if (playerInSightRange && !playerInAttackRange && !isAlerted) ChasePlayer();
             if (playerInSightRange && playerInAttackRange && !isAlerted) AttackPlayer();
             
             if (isAlerted && !playerInSightRange) GoToAlertPlace();
             if (playerInSightRangeAlerted && isAlerted) ChasePlayer();
             if(playerInAttackRangeAlerted && isAlerted) AttackPlayer();
    }

    [Button]
    private void GoToAlertPlace()
    {
        //yürü  
        enemyNavMeshAgent.SetDestination(alertPlace.transform.position);
    }


    private void AttackPlayer()
    {
        //saldır
        transform.LookAt(player);
        enemyAnimator.SetBool("Attack",true);
    }
    


    private void ChasePlayer()
    {
        //kos
        enemyAnimator.SetBool("Attack",false);
        enemyNavMeshAgent.SetDestination(player.position);
     
    }

    private void Patroling()
    {
        //yürü
        if (Vector3.Distance(transform.position, nextPosition) <= 1f)
        {
            nextPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }
        enemyNavMeshAgent.SetDestination(nextPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    
}