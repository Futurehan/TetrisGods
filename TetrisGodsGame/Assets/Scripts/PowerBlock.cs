using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlock : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        print("HERE");
        print(other.name);
        Cannon cannon = other.GetComponentInChildren<Cannon>();
        if (cannon != null)
        {
            cannon.Shoot();
        }
    }
}
