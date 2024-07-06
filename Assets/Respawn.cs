using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Zorag zorag;
    public AudioClip waterSplash;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(waterSplash);
            zorag.HasPlayerFallen(true);
        }
    }
}
