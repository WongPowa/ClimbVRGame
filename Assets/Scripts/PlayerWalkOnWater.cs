using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkOnWater : MonoBehaviour
{
    AudioManager audioManager;
    public CharacterController cc;
    float positionY;
    float threshold = -0.4f;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        positionY = cc.transform.position.y;
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        positionY = cc.transform.position.y;
        if (transform.position.y < threshold)
        {
            audioManager.PlaySFX(audioManager.inWaterSound);
        }
    }

    private IEnumerator PlayWaterSound()
    {
        yield return new WaitForSeconds(2);
        audioManager.PlaySFX(audioManager.inWaterSound);
    }
}
