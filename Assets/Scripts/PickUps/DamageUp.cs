using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageUp : MonoBehaviour {

    public int BoostValue;

    private AudioSource powerupSound;
    private ParticleSystem particles;
	
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
            other.gameObject.GetComponentInChildren<PlayerShooting>().damagePerShot += BoostValue;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
