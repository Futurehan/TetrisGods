using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorKillZone : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        BlockController block = other.GetComponentInParent<BlockController>();
        if (block !=null)
        {
            block.Death();

        }

        Destroy(other.gameObject);
    }
}
