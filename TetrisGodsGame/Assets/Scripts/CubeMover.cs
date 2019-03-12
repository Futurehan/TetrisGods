using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public GameObject targetLocation;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.MoveTo(targetLocation.transform.position,0.1f,this);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.VectorAprox(targetLocation.transform.position,1))
        {
            print("Within range");
        }
    }
}
