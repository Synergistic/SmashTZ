using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public PickupManager pickupManager;

    Animator anim;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    bool autofire;


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gunParticles = GetComponentInChildren<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        autofire = false;
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("AutoFire"))
        {
            autofire = !autofire;
        }

        if ((Input.GetButton("Fire1") || Mathf.Abs(Input.GetAxis("XTrigger")) > 0 || autofire) && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunParticles.Stop();
        gunParticles.Play();

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        gunLine.enabled = true;
        gunLight.enabled = true;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(0, transform.position);
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(0, transform.position);
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    public void ReduceFireTime(float amount)
    {
        if (timeBetweenBullets > .06f)
        {
            timeBetweenBullets -= amount;
        }
        else
        {
            pickupManager.RemoveItemByName("RateOfFire");
        }
        
    }
}
