using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DKBarrel : MonoBehaviour
{
    Rigidbody body;
    public GameObject thrustEffect;
    private bool active = false;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (active == true)
        {
            if (body.velocity.magnitude < 0.1f)
            {
                thrustEffect.SetActive(false);

            }

        }


    }

    public void Fire(Vector3 forceDirection)
    {
        Vector3 shootForce = forceDirection.normalized * 10f;
        body.AddForce(shootForce,ForceMode.Impulse);
        StartCoroutine(startTimer());
    }

    private IEnumerator startTimer()
    {
        yield return new WaitForSeconds(1f);
        active = true;
    }

}
