using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingSound : MonoBehaviour
{
    public AudioManager audioManager;
    bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlaySFX(audioManager.grabSound);
        hasPlayed = true;
    }

    // Update is called once per frame
    void Update()
    {
        //need to ge tthe HAND INPUT o(-(
        if (hasPlayed == false)
        {
            audioManager.PlaySFX(audioManager.grabSound);
        }
        else
        {
            hasPlayed = !hasPlayed;
        }
    }
}
