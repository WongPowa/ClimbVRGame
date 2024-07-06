using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayWalkSound : MonoBehaviour
{
    AudioManager audioManager;
    public CharacterController cc;
    float overallSpeed, nowSpeed, diffSpeed;
    bool audioPlayed = false;
    //private IEnumerator coroutine;
    Coroutine co = null;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        co = StartCoroutine(plsOmg());
    }


    private IEnumerator plsOmg()
    {
        yield return new WaitForSeconds(2);
        overallSpeed = cc.velocity.magnitude;
        diffSpeed = overallSpeed - nowSpeed;
        if (diffSpeed > 0.0f)
        {
            Debug.Log("meow");
            if (audioPlayed == false)
            {
                Debug.Log("haizz");
                //audioManager.PlaySFX(audioManager.walk);
                //audioPlayed = true;
                //Debug.Log("start boi");
                if (co != null)
                {
                    Debug.Log("heya");
                    co = StartCoroutine(PlaySound());
                }
                else
                {
                    Debug.Log("end");
                    StopCoroutine(co);
                    co = null;
                }


            }

        }
        nowSpeed = cc.velocity.magnitude;
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(2);
        audioManager.PlaySFX(audioManager.walkingSound);
        StartCoroutine(Wait());
        //audioPlayed = true;
        
        Debug.Log("start boi");        
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        //audioManager.PlaySFX(audioManager.walk);
        //audioPlayed = !audioPlayed;
        Debug.Log("STOP 2 SEC");
    }

    void random()
    {
        //avgSpeed = 0;

        overallSpeed = cc.velocity.magnitude;
        if (overallSpeed > 0.0f)
        {
            if (audioPlayed == true)
            {
                StartCoroutine(Wait());
                audioPlayed = false;
            }
            else
            {
                //if (overallSpeed > 0.0f)
                //{
                StartCoroutine(PlaySound());
                audioPlayed = true;
                //}

            }

        }
    }
}