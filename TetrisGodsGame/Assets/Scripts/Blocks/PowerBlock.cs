using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlock : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            IPowerUp cannon = other.GetComponentInChildren<IPowerUp>();
            if (cannon != null)
            {
                cannon.Shoot();
                cannon = null;
            }
        }
       
    }
}
