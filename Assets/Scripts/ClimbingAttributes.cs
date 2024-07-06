using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingAttributes : MonoBehaviour
{
    [SerializeField] private float m_climbingDifficulty = 1;

    public float climbingDifficulty => m_climbingDifficulty;
}
