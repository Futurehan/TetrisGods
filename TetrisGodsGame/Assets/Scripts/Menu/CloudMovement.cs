using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetPosition = gameObject.transform.position;
        targetPosition.x += 20f;
        gameObject.MoveTo(targetPosition,Random.Range(0.01f,0.1f),this);
    }


}
