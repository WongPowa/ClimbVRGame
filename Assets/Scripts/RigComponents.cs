using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RigComponents : MonoBehaviour
{
    public XRDirectInteractor leftHandInteractor;
    public XRDirectInteractor rightHandInteractor;

    public XRDirectInteractor GetOtherDirectInteractor(XRDirectInteractor currentInteractor)
    {
        if (currentInteractor == leftHandInteractor)
            return rightHandInteractor;
        else if (currentInteractor == rightHandInteractor)
            return leftHandInteractor;

        return null;
    }
}