using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public NavMeshAgent enemyNavMeshAgent;
    public Transform player;
    public LayerMask whatIsPlayer;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange,isAlerted,playerInAttackRangeAlerted;

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
       
        
             if (playerInSightRange && !playerInAttackRange && !isAlerted) ChasePlayer();
             if (playerInSightRange && playerInAttackRange && !isAlerted) AttackPlayer();
             if (!playerInSightRange && !playerInAttackRange && !isAlerted) Patroling();
             if (isAlerted) GoToAlertPlace();
    }

    private void GoToAlertPlace()
    {
        enemyNavMeshAgent.SetDestination(alertPlace.transform.position);
    }


    private void AttackPlayer()
    {
        transform.LookAt(player);
        enemyAnimator.SetBool("Attack",true);
    }
    


    private void ChasePlayer()
    {
        
        enemyAnimator.SetBool("Attack",false);
        enemyNavMeshAgent.SetDestination(player.position);
     
    }

    private void Patroling()
    {
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