 using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue = 10;
    

    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    EnemyManager enemyManager;
    PickupManager pickupManager;
    Animator anim;
    EnemyMovement enemyMovement;
    bool isDead;
    bool isSinking;
    bool rolledAlready;


    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        pickupManager = GameObject.Find("PickupManager").GetComponent<PickupManager>();
        currentHealth = startingHealth;
        isSinking = false;
        rolledAlready = false;
    }


    void Update()
    {
        if (isSinking)
        {
            //we don't care about physics when animating sinking, thus Translate
            transform.Translate(-Vector3.up * 2.5f * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {

        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= ((startingHealth / 4) * 3))
        {
            anim.SetBool("isRunner", true);
            enemyMovement.SetSpeed(2.5f);
        }

        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        //so player can't hit dead/dying enemies
        gameObject.layer = LayerMask.NameToLayer("Default");
        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");
        ScoreManager.score += scoreValue;
        if (!rolledAlready)
        {
            rolledAlready = true;
            pickupManager.RollForItem(transform.position);
        }
            
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true; //stops recalculating geometry for this object
        isSinking = true;
        Invoke("ResetEnemy", 3f);

    }

    void ResetEnemy()
    {
        transform.position = new Vector3(0f, -10f, 0f);
        currentHealth = startingHealth;
        isDead = false;
        capsuleCollider.isTrigger = false;
        GetComponent<Rigidbody>().isKinematic = false;
        isSinking = false;
        rolledAlready = false;
        enemyMovement.SetSpeed(1f);
        gameObject.layer = LayerMask.NameToLayer("Shootable");
        enemyManager.ReturnEnemyToPool(gameObject);
    }

    public void SetEnemyHealth(int value)
    {
        startingHealth += value;
        currentHealth = startingHealth;
    }

}
