using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    public AudioClip[] death_sound = new AudioClip[5];
    public AudioClip[] critical_sound = new AudioClip[3];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = death_sound[Random.Range(0, death_sound.Length)];
        source.Play();
    }
    public void Do_critical()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = critical_sound[Random.Range(0, critical_sound.Length)];
        source.Play();
    }
}
