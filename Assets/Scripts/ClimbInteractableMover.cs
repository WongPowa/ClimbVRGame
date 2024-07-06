using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using Unity.XR.CoreUtils;
using System;

public class ClimbInteractableMover : MonoBehaviour
{
    [SerializeField] private Transform startPosition, endPosition;
    [SerializeField] private ClimbInteractable climbInteractable;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private AnimationCurve forwardAnimCurve = AnimationCurve.Linear(0f, 1f, 1f, 1.5f);
    [SerializeField] private AnimationCurve returnAnimCurve = AnimationCurve.Linear(0f, 1f, 1f, 1);

    private float m_MovementThreshold = 0.01f;
    private Coroutine m_MovementCoroutine;
    private Vector3 m_Offset;
    private XROrigin m_XRRig;
    public AudioSource audio;

    private void Awake()
    {
        OnValidate();
        CheckVariables();

        climbInteractable.selectEntered.AddListener(OnGrab);
        climbInteractable.selectExited.AddListener(OnRelease);
        m_Offset = transform.position - startPosition.position;
    }

    private void OnValidate()
    {
        if (!climbInteractable)
            TryGetComponent(out climbInteractable);
    }
    private void CheckVariables()
    {
        if (!climbInteractable)
        {
            Debug.LogError($"{gameObject.name}: {nameof(ClimbInteractable)} component not found");
            enabled = false;
        }

        if (!startPosition || !endPosition)
        {
            Debug.LogError($"{gameObject.name}: Start or End Position not set", this);
            enabled = false;
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        m_XRRig = null; // Stop moving the player
        audio.loop = false;
        audio.Stop();
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        m_XRRig = args.interactorObject.transform.GetComponentInParent<XROrigin>();
        ReleaseOtherHandIfGrabbingClimbable();
        audio.Play();
        audio.loop = true;
        StartMovement();
    }

    private void ReleaseOtherHandIfGrabbingClimbable()
    {
        var currentInteractor = climbInteractable.firstInteractorSelecting.transform.GetComponent<XRDirectInteractor>();
        var otherHand = m_XRRig.GetComponent<RigComponents>().GetOtherDirectInteractor(currentInteractor);

        if (!otherHand.hasSelection) return;

        var otherClimbInteractable = otherHand.firstInteractableSelected as ClimbInteractable;
        if (otherClimbInteractable != null)
            climbInteractable.interactionManager.SelectExit((IXRSelectInteractor)otherHand, otherClimbInteractable);
    }

    private void StartMovement()
    {
        if (m_MovementCoroutine != null)
            StopCoroutine(m_MovementCoroutine);

        m_MovementCoroutine = StartCoroutine(MoveToPosition(endPosition, forwardAnimCurve, true));
    }

    private void ReturnToStart()
    {
        if (m_MovementCoroutine != null)
            StopCoroutine(m_MovementCoroutine);

        m_MovementCoroutine = StartCoroutine(MoveToPosition(startPosition, returnAnimCurve, false));
    }

    private IEnumerator MoveToPosition(Transform targetPosition, AnimationCurve curve, bool movePlayer)
    {
        Vector3 startingPosition = transform.position;
        Vector3 targetWithOffset = targetPosition.position + m_Offset;
        Vector3 lastPosition = transform.position;

        float totalDistance = Vector3.Distance(startingPosition, targetWithOffset);
        float distanceCovered = 0f;

        while (Vector3.Distance(transform.position, targetWithOffset) > m_MovementThreshold)
        {
            float speedFactor = curve.Evaluate(distanceCovered / totalDistance) * movementSpeed;
            distanceCovered += Time.deltaTime * speedFactor;
            transform.position = Vector3.Lerp(startingPosition, targetWithOffset, distanceCovered / totalDistance);

            UpdatePlayerPosition(movePlayer, ref lastPosition);

            yield return null;
        }

        transform.position = targetWithOffset;
        UpdatePlayerPosition(movePlayer, ref lastPosition);

        while (m_XRRig)
            yield return null;
        if (targetPosition == endPosition) ReturnToStart(); // Automatically return to start
    }

    private void UpdatePlayerPosition(bool movePlayer, ref Vector3 lastPosition)
    {
        if (!m_XRRig || !movePlayer) return;

        Vector3 movement = transform.position - lastPosition;
        m_XRRig.transform.position += movement;
        lastPosition = transform.position;
    }
}