using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Zorag : MonoBehaviour
{
    private Animator anim;
    public Transform[] points;
    private int current = 1;
    public float speed = 2.0f;
    public float rotationSpeed = 5.0f;
    public float steeringStrength = 0.5f;
    private bool hasPlayerFallen;
    [SerializeField] private Transform player;
    [SerializeField] private Player playerScript;
    [SerializeField] private Animator fadeAnimator;
    

    private Vector3 currentVelocity;
    public AudioClip chompSound;
    public AudioClip roarSound;
    AudioSource audio;

    void Awake()
    {
         audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        hasPlayerFallen = false;
        anim = GetComponent<Animator>();
        anim.speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Length == 0)
            return;

        if (!hasPlayerFallen)
        {
            MoveTowardsPoints();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPoints()
    {
        
        Vector3 targetDirection = points[current].position - transform.position;

        if (targetDirection.magnitude > 0.1f)
        {
            targetDirection.Normalize();

            // Apply steering behavior
            Vector3 desiredVelocity = targetDirection * speed;
            Vector3 steering = desiredVelocity - currentVelocity;
            steering = Vector3.ClampMagnitude(steering, steeringStrength);

            currentVelocity = Vector3.ClampMagnitude(currentVelocity + steering, speed);
            audio.Play();
            audio.loop = true;
            transform.position += currentVelocity * Time.deltaTime;

            if (currentVelocity != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(currentVelocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            current = (current + 1) % points.Length;
        }
    }

    void MoveTowardsPlayer()
    {
        
        Vector3 targetDirection = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z);

        if (targetDirection.magnitude > 0.1f)
        {
            targetDirection.Normalize();

            // Apply steering behavior
            Vector3 desiredVelocity = targetDirection * speed;
            Vector3 steering = desiredVelocity - currentVelocity;
            steering = Vector3.ClampMagnitude(steering, steeringStrength);

            currentVelocity = Vector3.ClampMagnitude(currentVelocity + steering, speed);

            transform.position += currentVelocity * Time.deltaTime;

            if (currentVelocity != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(currentVelocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void HasPlayerFallen(bool boolean)
    {
        hasPlayerFallen = boolean;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audio.PlayOneShot(chompSound);
            audio.loop = false;
            fadeAnimator.SetTrigger("TriggerFade");
            StartCoroutine(playerFallenDelay());
            StartCoroutine(RespawnWithDelay());
        }
    }

    private IEnumerator playerFallenDelay()
    {
        yield return new WaitForSeconds(0.5f);
        hasPlayerFallen = false;
        //audioManager.PlaySFX(audioManager.bite);

    }
    private IEnumerator RespawnWithDelay()
    {
        yield return new WaitForSeconds(1.0f);
        playerScript.respawn();
    }
}
