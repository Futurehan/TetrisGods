using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperWeapon : MonoBehaviour, IPowerUp
{
    public GameObject Destroyer;
    GameObject dk;
    public void Shoot()
    {
        dk = Instantiate(Destroyer, gameObject.transform.position, Quaternion.identity);
        dk.GetComponent<ProDestroyer>().Fire(gameObject.transform.up);
    }
}
