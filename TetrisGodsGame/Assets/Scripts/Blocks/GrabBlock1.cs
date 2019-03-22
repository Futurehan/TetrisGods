using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBlock1 : MonoBehaviour, IPowerUp
{
    public bool pulling = false;
    public StuckBlock[] stucks;
    public GameObject explosion;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public AudioSource bzztSFX;


    public void Shoot()
    {
        bzztSFX.Play();
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
    public void Death()
    {
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        foreach (StuckBlock s in stucks)
        {
            if (s!=null)
            {
                Destroy(s);
            }
        }
        foreach (Rigidbody b in rigidbodies)
        {
            if (b!=null)
            {
                b.gameObject.GetComponent<Rigidbody>().isKinematic = false;
//                print(b.gameObject.name);
            }
        }
    }
}
