using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProDestroyer : MonoBehaviour
{
    Rigidbody body;
    BoxCollider boxy;
    public GameObject explosion;
    public int blocksToDestroy;
    int blocksDestroyed;
    public AudioSource splosionSFX;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }


    public void Fire(Vector3 forceDirection)
    {
        Vector3 shootForce = forceDirection.normalized * 7f;
        body.AddForce(shootForce, ForceMode.Impulse);
//        print("KILLER");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (blocksDestroyed >= blocksToDestroy) return;

        //Determine parent of collided object
        Transform parent = collision.transform.parent;
        if (parent)
        {
            BlockController parentController = GetComponentInParent<BlockController>();
   
            for (int i = 0; i < parent.childCount; i++)
            {
                GameObject subBox = parent.GetChild(i).gameObject;
                if (subBox != collision.gameObject && subBox.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody rb = parent.GetChild(i).gameObject.AddComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
                }
                else if (subBox != collision.gameObject && subBox.GetComponent<Rigidbody>() !=null)
                {
                    subBox.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
            
            if (parentController != null && parentController.Active)
                parentController.Spawn();         
            
        }

        //Destroy collided blocks and count how many

        Destroy(collision.gameObject);
        splosionSFX.Play();
        Instantiate(explosion,collision.gameObject.transform.position,Quaternion.identity);
        blocksDestroyed++;
    }
}