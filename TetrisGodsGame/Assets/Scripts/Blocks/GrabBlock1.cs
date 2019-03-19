using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBlock1 : MonoBehaviour, IPowerUp
{
    public bool pulling = false;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();


    public void Shoot()
    {
        GetComponentInParent<Rigidbody>().isKinematic = true;
        if (GetComponentInParent<BlockController>().Active == true)
        {
            GetComponentInParent<BlockController>().Spawn();
        }
        pulling = true;
    }
    public void AddBody(Rigidbody b)
    {
        rigidbodies.Add(b);
    }
    private void OnDestroy()
    {
        foreach (Rigidbody b in rigidbodies)
        {
            if (b!=null)
            {
                b.isKinematic = false;

            }
        }
    }
}
