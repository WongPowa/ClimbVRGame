using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ClimbingStaminaController : MonoBehaviour
{
    public UnityEvent GrabStarted, GrabEnded;
    [SerializeField] private XRBaseInteractor interactor;
    [SerializeField] private DynamicMoveProvider dynamicMoveProvider;
    [SerializeField] private float m_MaxStamina = 100, staminaConsumptionRate = 5, staminaRegenRate = 5;
    [SerializeField] private float minStaminaToGrab = 5;

    private float m_CurrentStamina, m_ClimbingDifficultly;
    private bool m_IsClimbing;
    private ClimbInteractable m_CurrentClimbInteractable;

    private static int s_ClimbingHandCount = 0; // Static counter to track how many hands are climbing
    public AudioClip lowStaminaSound;

    public float MaxStamina => m_MaxStamina;
    public float CurrentStamina => m_CurrentStamina;

    private void Awake()
    {
        OnValidate();
        interactor.selectEntered.AddListener(TryStartClimbing);
        interactor.selectExited.AddListener(InteractableReleased);
        m_CurrentStamina = m_MaxStamina;
    }

    private void OnValidate()
    {
        if (!interactor)
            TryGetComponent(out interactor);
        if (!dynamicMoveProvider)
            dynamicMoveProvider = GetComponentInParent<XROrigin>().GetComponentInChildren<DynamicMoveProvider>();
    }

    private void Update()
    {
        if (m_IsClimbing)
        {
            ConsumeStamina();
            StopClimbingIfStaminaIsLow();
        }
        else
        {
            RegenStamina();
        }
    }

    private void RegenStamina()
    {
        m_CurrentStamina = Mathf.Min(m_MaxStamina, m_CurrentStamina + staminaRegenRate * Time.deltaTime);
    }

    private void StopClimbingIfStaminaIsLow()
    {
        if (!HasEnoughStamina(staminaConsumptionRate))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(lowStaminaSound);
            ReleaseObject();
        }

           
    }

    private void ConsumeStamina()
    {
        var staminaDrain = staminaConsumptionRate * m_ClimbingDifficultly * Time.deltaTime;
        m_CurrentStamina = Mathf.Max(0f, m_CurrentStamina - staminaDrain);
    }

    private void TryStartClimbing(SelectEnterEventArgs arg0)
    {
        if (!arg0.interactableObject.transform.TryGetComponent(out ClimbInteractable climbInteractable)) return;
        if (!climbInteractable.TryGetComponent(out ClimbingAttributes climbingAttributes)) return;

        if (!HasEnoughStamina(minStaminaToGrab))
        {
            ReleaseObject();
            return;
        }

        m_IsClimbing = true;
        m_CurrentClimbInteractable = climbInteractable;
        m_ClimbingDifficultly = climbingAttributes.climbingDifficulty;
        s_ClimbingHandCount++; // Increment the climbing hand counter
        if (s_ClimbingHandCount == 1)
        {
            StartCoroutine(DisableGravity());
            GrabStarted.Invoke();
        }
    }

    private void InteractableReleased(SelectExitEventArgs arg0)
    {
        if (!arg0.interactableObject.transform.TryGetComponent(out ClimbInteractable climbInteractable)) return;
        if (!climbInteractable.TryGetComponent(out ClimbingAttributes climbingAttributes)) return;

        ReleaseObject();
    }

    private void ReleaseObject()
    {
        if (interactor.hasSelection && interactor.firstInteractableSelected as ClimbInteractable == m_CurrentClimbInteractable)
            interactor.interactionManager.SelectExit(interactor as IXRSelectInteractor, m_CurrentClimbInteractable);

        m_IsClimbing = false;
        m_CurrentClimbInteractable = null;
        s_ClimbingHandCount--; // Decrement the climbing hand counter
        if (s_ClimbingHandCount == 0)
        {
            StartCoroutine(EnableGravity());
            GrabEnded.Invoke();
        }
    }

    private bool HasEnoughStamina(float threshold)
    {
        return m_CurrentStamina > threshold;
    }

    private IEnumerator EnableGravity()
    {
        if (!dynamicMoveProvider)
            yield break;
        yield return null; // Ensure gravity is applied in the next frame
        dynamicMoveProvider.useGravity = true;
    }

    private IEnumerator DisableGravity()
    {
        if (!dynamicMoveProvider)
            yield break;
        yield return null; // Ensure gravity is disabled in the next frame
        dynamicMoveProvider.useGravity = false;
    }
}
