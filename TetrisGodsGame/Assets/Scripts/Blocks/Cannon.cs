using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, IPowerUp
{
    public GameObject Barrel;
    GameObject dk;
    public void Shoot()
    {
        dk = Instantiate(Barrel,gameObject.transform.position,gameObject.transform.rotation);
        dk.GetComponent<DKBarrel>().Fire(gameObject.transform.up);
    }



}
