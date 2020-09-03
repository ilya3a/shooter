
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public enum EnemyState
    {
        PATROL,
        CHASE,
        ATTACK
    }
public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navAgent;

    [SerializeField]public EnemyState eEnemyState;
    

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;
    public float chaseDistance = 7f;

    public float attackDistance = 1.8f;
    public float chaseAfterAttackDistance = 2f;//the distance to leave between the enemy and me before start chasing
    private float currentChaseDistance;

    public float patroladiusMin = 20f, patrollRadiusMax = 60f;
    public float patrolForThisTime = 15f;
    private float patrolTimer;

    public float waitBeforeAttack = 0f;
    private float attackTimer;

    private Transform target;

    public GameObject attackPoint;

    private EnemyAudio enemyAudio;


    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        eEnemyState = EnemyState.PATROL;

        patrolTimer = patrolForThisTime;
        //when the enemy first gets to the player 
        //attack right away
        attackTimer = waitBeforeAttack;
        //memorize the value so that we can put it back
        currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (eEnemyState)
        {
            case EnemyState.PATROL:
                Patrol();
                break;
            case EnemyState.CHASE:
                Chase();
                break;
            case EnemyState.ATTACK:
                Attack();
                break;
        }
    }

    private void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attackTimer += Time.deltaTime;
        if (attackTimer >= waitBeforeAttack)
        {
            attackTimer = 0f;
            enemyAnimator.Attack();
            //play the sound of attack
        }

        if(Vector3.Distance(transform.position, target.position) > attackDistance + chaseAfterAttackDistance)
        {
            eEnemyState = EnemyState.CHASE;
            enemyAnimator.Run(true);
        }
    }

    private void Chase()
    {
        //enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        //setting the player position as destination because we running towards the player
        navAgent.SetDestination(target.position);
        //need to animate the movment
        if (navAgent.velocity.sqrMagnitude > 0)
        {//moving 
            enemyAnimator.Run(true);
        }
        else
        {
            enemyAnimator.Run(false);
        }
        //test the distance if it is suttible fo attack
        if (Vector3.Distance(target.position,navAgent.transform.position) <= attackDistance)
        {//going to attack
            //stop the animations
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            eEnemyState = EnemyState.ATTACK;

            if(chaseDistance != currentChaseDistance)
            {//reset the chase distance to previus
                chaseDistance = currentChaseDistance;
            }
        }else if(Vector3.Distance(transform.position,target.position) > chaseDistance)
        {//player running away from enemy

            //stop running
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            eEnemyState = EnemyState.PATROL;

            //rest the patorol timer that the func do ne patrol destination
            patrolTimer = patrolForThisTime;

            //reset the patrol chase distance
            if (chaseDistance != currentChaseDistance)
            {//reset the chase distance to previus
                chaseDistance = currentChaseDistance;
            }
        }
    }

    private void Patrol()
    {
        //tell the nav agent that he can move
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        patrolTimer += Time.deltaTime;
        //when patrolTimer gets bigger than patrolForThisTime we will set new destination
        if (patrolTimer > patrolForThisTime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;

        }
        //need to animate the movment
        if (navAgent.velocity.sqrMagnitude > 0)
        {//moving 
            enemyAnimator.Walk(true);
        }
        else
        {
            enemyAnimator.Walk(false);
        }
        //test the distance between the player and the enemy
        if(Vector3.Distance(transform.position,target.position) <= chaseDistance)
        {
            enemyAnimator.Walk(false);
            eEnemyState = EnemyState.CHASE;

            //play spotted audio
        }

    }

    private void SetNewRandomDestination()
    {
        //generate random radius
        var randRadius = UnityEngine.Random.Range(patroladiusMin, patrollRadiusMax);
        //generate random point on sphere
        var randDirection = UnityEngine.Random.insideUnitSphere * randRadius;
        //add this point to the current location
        randDirection += transform.position;

        //to prevent option the enemy go outside the walking area 
        NavMeshHit navMeshHit;
        //test if the next random position is out of the area
        NavMesh.SamplePosition(randDirection, out navMeshHit, randRadius, -1);

        navAgent.SetDestination(navMeshHit.position);


    }
    // works from the animation
    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
