using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Pull : MonoBehaviour
{
    public CharacterController character;
    private bool isAttached;
    public static XRController climbingHand;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (isAttached)
        {
            //UnityEngine.VR.InputTracking.disablePositionalTracking = false;
            Climb();
        } else
        {
            //continuousMovement.enabled = true;
        }
    }

    void Climb()
    {
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }
}