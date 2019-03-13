using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class LerpExtension
{
    public static void MoveTo(this GameObject objectToMove, Vector3 targetLocation, float speed,MonoBehaviour mono)
    {
        mono.StartCoroutine(moveCube(objectToMove, targetLocation, speed));

    }

    public static bool VectorAprox(this Vector3 firstVector, Vector3 secondVector, float errorMarginal)
    {
        if (firstVector.x <=secondVector.x + errorMarginal && firstVector.x >= secondVector.x - errorMarginal && firstVector.y <= secondVector.y + errorMarginal && firstVector.y >= secondVector.y - errorMarginal && firstVector.z <= secondVector.z + errorMarginal && firstVector.z >= secondVector.z - errorMarginal)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
    public static IEnumerator moveCube(GameObject objectToMove, Vector3 targetLocation, float speed)
    {
        float currentPath = 0;
        
        Vector3 initalLocation = objectToMove.transform.position;

        while (initalLocation != targetLocation)
        {
            objectToMove.transform.position = Vector3.Lerp(initalLocation, targetLocation, currentPath);
            currentPath = currentPath + speed * Time.deltaTime;
            yield return null;
        }
    }
}
