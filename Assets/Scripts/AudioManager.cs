using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Sources ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource ambienceSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Sources ----------")]
    public AudioClip backgroundMusic;
    public AudioClip ambienceSound;
    public AudioClip walkingSound;
    public AudioClip grabSound;
    public AudioClip inWaterSound;
    public AudioClip staminaSound;
    public AudioClip ziplineSound;
    public AudioClip bite;

    private void Start()
    {
        //musicSource.clip = backgroundMusic;
        //musicSource.Play();

        // ambienceSource.clip = ambienceSound;
        // ambienceSource.Play();

    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
