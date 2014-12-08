using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {


    public AudioClip[] zombieClips;
    public float minInterval;
    public float maxInterval;

    private AudioSource zombieSound;
    private float timer;
    private Queue<AudioClip> soundQ;
    private float thisInterval;
    private PlayerHealth playerHealth;

	// Use this for initialization
	void Start () 
    {
        playerHealth = GameObject.Find("Soldier").GetComponent<PlayerHealth>();
        soundQ = new Queue<AudioClip>(zombieClips.Length);
        zombieSound = GetComponent<AudioSource>();
	    timer = 0;
        foreach (AudioClip zombieClip in zombieClips)
        {
            soundQ.Enqueue(zombieClip);
        }
        thisInterval = Random.Range(minInterval, maxInterval);

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (playerHealth.currentHealth > 0)
        {
            timer += Time.deltaTime;
            if (timer >= thisInterval)
            {
                timer = 0;
                AudioClip currentClip = soundQ.Dequeue();
                zombieSound.clip = currentClip;
                zombieSound.Play();
                soundQ.Enqueue(currentClip);
                thisInterval = Random.Range(minInterval, maxInterval);

            }
        }

	}
}
