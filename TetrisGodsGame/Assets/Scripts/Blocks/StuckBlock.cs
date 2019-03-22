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
    private void OnDestroy()
    {
        if (master !=null)
        {
            master.Death();

        }

    }

    private void OnTriggerEnter(Collider other)
    {
 
      

        if (master.pulling == true)
        {
            Rigidbody body = null;
            body = other.gameObject.GetComponentInParent<Rigidbody>();
            if (body != null)
            {
                master.AddBody(body);

                body.isKinematic = true;
            //    print("Making body kinematic");

            }
            BlockController controller = other.GetComponentInParent<BlockController>();
            if (controller != null)
            {
                if (controller.Active)
                {
                    other.GetComponentInParent<BlockController>().Spawn();

                }

            }

        }

    
      

        }

    }
  


