using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
//using UnityEngine.XR.InputTracking;

public class GrabSound : MonoBehaviour
{
    public AudioManager audioManager;
    //public Xr
    public UnityEngine.XR.InputDevice device;
    List<InputDevice> inputDevices;
    InputDeviceCharacteristics deviceRole = InputDeviceCharacteristics.Left;
    InputFeatureUsage<Vector2> inputFeatureAxis = CommonUsages.primary2DAxis;
    [SerializeField] Vector2 axisValue;
    void Awake()
    {
        inputDevices = new List<InputDevice>();
    }
    void Update()
    {
        InputDevices.GetDevicesWithCharacteristics(deviceRole, inputDevices);
        for (int i = 0; i < inputDevices.Count; i++)
        {
            if (inputDevices[i].TryGetFeatureValue(inputFeatureAxis, out axisValue))
            {
                Debug.Log(axisValue);
            }
        }
    }


    ////if (Input.GetButton("gripButton"))
    ////{
    ////    audioManager.PlaySFX(audioManager.grab);
    ////}

    //var gameControllers = new List<UnityEngine.XR.InputDevice>();
    //UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics((UnityEngine.XR.InputDeviceCharacteristics)UnityEngine.XR.InputDeviceRole.GameController, gameControllers);

    //foreach (var device in gameControllers)
    //{
    //    Debug.Log(string.Format("Device name '{0}' has role '{1}'", device.name, device.role.ToString()));
    //}

    ////var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
    ////UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

    ////if (leftHandDevices.Count == 1)
    ////{
    ////    UnityEngine.XR.InputDevice device = leftHandDevices[0];
    ////    Debug.Log(string.Format("Device name '{0}' with role '{1}'", device.name, device.role.ToString()));
    ////}
    ////else if (leftHandDevices.Count > 1)
    ////{
    ////    Debug.Log("Found more than one left hand!");
    ////}

    //bool triggerValue;
    //if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
    //{
    //    Debug.Log("Trigger button is pressed.");
    //}
    //else
    //{
    //    //Debug.Log("AAAAAA");
    //}
}
