using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProDestroyer : MonoBehaviour
{
    Rigidbody body;
    BoxCollider boxy;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        boxy = GetComponent<BoxCollider>();
    }


    public void Fire(Vector3 forceDirection)
    {
        Vector3 shootForce = forceDirection.normalized * 30f;
        body.AddForce(shootForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        boxy.enabled = false;
        print("pls die");
    }
}
