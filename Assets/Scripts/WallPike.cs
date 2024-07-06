using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pike")
        {
            
        }
    }
}
