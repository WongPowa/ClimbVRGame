using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class ClimbiingStaminaView : MonoBehaviour
{
    [SerializeField] private ClimbingStaminaController climbingStaminaController;
    [SerializeField] private MeshRenderer[] meshes;
    [SerializeField] private Material noStaminaMaterial, hasStaminaMaterial;
    [SerializeField] private float animationTime = .1f;

    private void Awake()
    {
        OnValidate();
        climbingStaminaController.GrabStarted.AddListener(() => SetDisplayActive(true));
        climbingStaminaController.GrabEnded.AddListener(() => SetDisplayActive(false));

        foreach (var mesh in meshes)
        {
            mesh.gameObject.SetActive(false);
        }
    }
    private void OnValidate()
    {
        if (!climbingStaminaController)
            TryGetComponent(out climbingStaminaController);
    }
    private void Update() => SetStaminaColorBlock(climbingStaminaController.CurrentStamina, climbingStaminaController.MaxStamina);


    private void SetStaminaColorBlock(float currentStamina, float maxStamina)
    {
        var staminaPerBlock = maxStamina / meshes.Length;
        for (int i = 0; i < meshes.Length; i++)
        {
            bool hasStamina = currentStamina > i * staminaPerBlock;
            meshes[i].material = hasStamina ? hasStaminaMaterial : noStaminaMaterial;
        }
    }
    private void SetDisplayActive(bool state)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayBlocks(state));
    }
    private IEnumerator DisplayBlocks(bool show)
    {
        foreach (var mesh in meshes)
        {
            if (mesh.gameObject.activeInHierarchy == show) continue;
            mesh.gameObject.SetActive(show);
            yield return new WaitForSeconds(animationTime);
        }
    }
}
