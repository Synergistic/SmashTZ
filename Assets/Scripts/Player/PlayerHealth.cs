using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public int startingHealth = 100;
    public int currentHealth;
    public Image healthFill;
    public Slider healthSlider;
    public AudioClip deathClip;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.2f);
    public GameOverManager gameOver;

    bool damaged;
    bool isDead;

    Animation anim;
    PlayerController playerController;
    PlayerShooting playerShooting;
    AudioSource playerAudio;


    void Awake()
    {
        anim = GetComponent<Animation>();
        playerAudio = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void GiveHealth(int amount)
    {
        if (!isDead && currentHealth < startingHealth)
        {
            currentHealth += amount;
            if (currentHealth > 100) currentHealth = 100;
            healthSlider.value = currentHealth;
        }

    }

    void Death()
    {
        isDead = true;
        healthFill.color = Color.clear;
        string[] possibleDeaths = new string[] { "soldierDieBack", "soldierDieFront" };
        anim.CrossFade(possibleDeaths[Random.Range(0,2)]);
        
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerController.enabled = false;
        playerShooting.DisableEffects();
        playerShooting.enabled = false;

        gameOver.GameOver();

    }

}
