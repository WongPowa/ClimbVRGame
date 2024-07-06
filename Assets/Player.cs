using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public Transform[] respawnPoints;
    private int current;

    private void Start()
    {
        current = 0;
    }

    public void respawn()
    {
        gameObject.transform.position = respawnPoints[current].position;
    }

    public void updateRespawnPoints(int position)
    {
        current = position;
        Debug.Log(current);
    }
    
}
