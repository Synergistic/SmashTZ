using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUp : MonoBehaviour
{

    public int BoostValue;
    private ParticleSystem particles;
    private PlayerHealth playerHealth;
    private AudioSource powerupSound;

    // Update is called once per frame
    void Awake()
    {
        powerupSound = GetComponent<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            particles.Play();
            powerupSound.Play();
            other.GetComponent<PlayerHealth>().GiveHealth(BoostValue);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
