
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperWeapon : MonoBehaviour, IPowerUp
{
    public GameObject Destroyer;
    GameObject bullet;
    public AudioSource shotSFX;

    public void Shoot()
    {
        shotSFX.Play();
        bullet = Instantiate(Destroyer, gameObject.transform.position, Quaternion.identity);
        bullet.GetComponent<ProDestroyer>().Fire(gameObject.transform.up);
    }
}
