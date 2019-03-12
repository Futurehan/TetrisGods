using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DKBarrel : MonoBehaviour
{
    Rigidbody body;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }


    public void Fire(Vector3 forceDirection)
    {
        Vector3 shootForce = forceDirection.normalized * 30f;
        body.AddForce(shootForce,ForceMode.Impulse);
    }

}
