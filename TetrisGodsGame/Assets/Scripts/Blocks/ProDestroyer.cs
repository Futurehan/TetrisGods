using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProDestroyer : MonoBehaviour
{
    Rigidbody body;
    BoxCollider boxy;
    public int blocksToDestroy;
    int blocksDestroyed;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }


    public void Fire(Vector3 forceDirection)
    {
        Vector3 shootForce = forceDirection.normalized * 7f;
        body.AddForce(shootForce, ForceMode.Impulse);
        print("KILLER");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (blocksDestroyed >= blocksToDestroy) return;

        //Determine parent of collided object
        Transform parent = collision.transform.parent;
        if (parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                GameObject subBox = parent.GetChild(i).gameObject;
                if (subBox != collision.gameObject && subBox.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody rb = parent.GetChild(i).gameObject.AddComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
                }


            }
        }

        //Destroy collided blocks and count how many
        Destroy(collision.gameObject); 
        print(collision + " DESTROYED!!!");
        blocksDestroyed++;
    }
}