using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConfetti : MonoBehaviour
{
    public ParticleSystem confetti;
    public AudioClip confettiAudio;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            confetti.Play();
            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(confettiAudio);
        }
    }
}
