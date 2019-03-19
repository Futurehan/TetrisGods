
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperWeapon : MonoBehaviour, IPowerUp
{
    public GameObject Destroyer;
    GameObject bullet;
    public void Shoot()
    {
        bullet = Instantiate(Destroyer, gameObject.transform.position, Quaternion.identity);
        bullet.GetComponent<ProDestroyer>().Fire(gameObject.transform.up);
    }
}
