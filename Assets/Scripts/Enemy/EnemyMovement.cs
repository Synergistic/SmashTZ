using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Animator anim;
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;


    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (!nav.pathPending) //if there isn't a pending path
            {
                if (nav.remainingDistance <= nav.stoppingDistance) //make sure we're within range of player
                {
                    if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                    {
                        anim.SetBool("chasingPlayer", false); //we are AT the player
                    }
                }
                else
                {
                    anim.SetBool("chasingPlayer", true); //otherwise we're chasing the player
                }
            }
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
            anim.SetBool("chasingPlayer", false);
        }
    }


    public void SetSpeed(float amount)
    {
        nav.speed = amount;
    }
}
