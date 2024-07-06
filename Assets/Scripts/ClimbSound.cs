using SerializableCallback;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbSound : MonoBehaviour
{
    [SerializeField] private ClimbingStaminaController climbingStaminaController;
    [SerializeField] private XRBaseInteractor interactor;
    public AudioClip climbSound;
    private void Awake()
    {
        OnValidate();
        interactor.selectEntered.AddListener(PlayClimbSound);
    }
    private void OnValidate()
    {
        if (!interactor)
            TryGetComponent(out interactor);
    }

    private void PlayClimbSound(SelectEnterEventArgs arg0)
    {
        if (!arg0.interactableObject.transform.TryGetComponent(out ClimbInteractable climbInteractable)) return;
        if (!climbInteractable.TryGetComponent(out ClimbingAttributes climbingAttributes)) return;
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(climbSound);
       
    }

    
}
