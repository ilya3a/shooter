using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private EnemyController enemyController;
    NavMeshAgent navMesh;

    [SerializeField] private float health = 100f;

    public bool isPlayer, isCannibal, isBoar;
    private bool isDead;
    // Start is called before the first frame update
    void Awake()
    {
        if( isCannibal || isBoar)
        {
            enemyAnimator = GetComponentInParent<EnemyAnimator>();
            enemyController = GetComponentInParent<EnemyController>();
            navMesh = GetComponentInParent<NavMeshAgent>();

            //get enemy audio
        }
        if (isPlayer)
        {
            
        }
    }

   public void ApllyDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        health -= damage;
        
        if (isPlayer)
        {
            //show the stats
        }
        if (isBoar || isCannibal)
        {
            if (enemyController.eEnemyState == EnemyState.PATROL)
            {
                enemyController.chaseDistance = 50f;
            }
        }

        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            isDead = true;
            if (isCannibal)
            {
                GetComponent<Animator>().enabled = false;
                GetComponentInChildren<MeshCollider>().isTrigger = false;
                //adding rotation for the cannibal looks daed
                GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);

                enemyController.enabled = false;
                navMesh.enabled = false;
                enemyAnimator.enabled = false;

                //start courutine

                //enemyManager spwan new enemy
            }
            if (isBoar)
            {
                //stop the boar
                navMesh.velocity = Vector3.zero;
                navMesh.isStopped = true;
                enemyController.enabled = false;
                enemyAnimator.Dead();
                //start courutine

                //enemyManager spwan new enemy
            }
            if (isPlayer)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
                foreach(GameObject enemy in enemies)
                {
                    enemy.GetComponent<EnemyController>().enabled = false;
                }
                //call enemy maneger to stop spawning enemies

                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                GetComponent<WeaponManager>().GetCurrentWeapon().gameObject.SetActive(false);

            }

            if(tag == Tags.PLAYER_TAG)
            {
                Invoke("RestartGame", 3f);
            }
            else
            {
                Invoke("TurnOffGameObject", 3f);
            }
        }
    }
    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
}
