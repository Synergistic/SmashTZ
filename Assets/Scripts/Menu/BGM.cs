using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour
{

    public AudioClip clip;
    private AudioSource source;
    private AudioSource[] sources;

    void Awake()
    {
        sources = GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (sources.Length == 0)
        {
            DontDestroyOnLoad(gameObject);
            source = gameObject.AddComponent("AudioSource") as AudioSource;
            source.playOnAwake = false;
            source.rolloffMode = AudioRolloffMode.Logarithmic;
            source.loop = true;
            source.clip = clip;
            source.volume = 1f;
            source.Play();
        }


    }


}
