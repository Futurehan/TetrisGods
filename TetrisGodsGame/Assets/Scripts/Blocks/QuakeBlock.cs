using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakeBlock : MonoBehaviour , IPowerUp
{

    GameObject targetPlatform;


    public void Shoot()
    {
       GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("1");
        //foreach (GameObject game in gameObjects)
        //{

        //}

        if (gameObject.transform.position.x < 0)
        {
            foreach (GameObject g in gameObjects)
            {
                if (g.transform.position.x > 0)
                {
                    targetPlatform = g;
                }
            }
        }
        else
        {
            foreach (GameObject g in gameObjects)
            {
                if (g.transform.position.x < 0)
                {
                    targetPlatform = g;
                }
            }
        }

        targetPlatform.GetComponentInChildren<Animation>()?.Play();

    }

}
