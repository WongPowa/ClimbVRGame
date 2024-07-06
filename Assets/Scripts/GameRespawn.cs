using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    public float threshold;

    void FixedUpdate()
    {
        if(transform.position.y < threshold)
        {
            //respawn the player at beginnning
            transform.position = new Vector3(-19.969f, 3.907f, -15.383f);
        }
    }
}
