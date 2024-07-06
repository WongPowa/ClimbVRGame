using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDuplication : MonoBehaviour
{
    public GameObject gameObjectPrefab;
    public int numberOfDuplications = 10;

    public Vector3 movementAmount, rotationAmount;

    private List<GameObject> duplicatedObjects = new();
    [Button()]
    public void RotateAndThenTranslate()
    {
        for(int i=0; i< numberOfDuplications; i++)
        {
            var newGameObject = Instantiate(gameObjectPrefab, transform.position, transform.rotation, transform);

            newGameObject.transform.Rotate(rotationAmount.x *(i+1), rotationAmount.y *(i+1), rotationAmount.z * (i + 1));
            newGameObject.transform.Translate(movementAmount);

            duplicatedObjects.Add(newGameObject);
        }
        
    }
    [Button()]
    public void Delete()
    {
        foreach(var obj in duplicatedObjects)
        {
            if(obj != null)
                DestroyImmediate(obj);
        }   
        duplicatedObjects.Clear();
    }
}
