using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckBlock : MonoBehaviour
{
    public GrabBlock1 master;
    public BlockController blockMaster;
    public GameObject Object;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (master.pulling == true)
        {
  
            other.gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;
            master.AddBody(other.gameObject.GetComponentInParent<Rigidbody>());


        }

    
      

        }

    }
  


