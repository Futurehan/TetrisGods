using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBlock : MonoBehaviour, IPowerUp
{
    public bool pulling = false;
    public float atractForce = 30f;
    private bool removeBody = true;
    private Rigidbody body;
    private List<Rigidbody> bodiesToPull =  new List<Rigidbody>();
    private SphereCollider colider;
    private BoxCollider box;
    private Vector3 hoverLocation;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponentInParent<Rigidbody>();
        colider = GetComponent<SphereCollider>();
        box = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        print(bodiesToPull.Count);
        if (pulling)
        {

            if (bodiesToPull.Count > 0)
            {


                foreach (Rigidbody b in bodiesToPull)
                {
                    Vector3 forceDirection = (gameObject.transform.position - b.transform.position).normalized * atractForce;
                    b.AddForce(forceDirection*Time.deltaTime);
                    print("Pulling");
                }
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody b = other.GetComponentInParent<Rigidbody>();

        if (b != null)
        {
            AddBodyToList(b);
            print("is not null");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Rigidbody b = other.GetComponentInParent<Rigidbody>();

        if (b != null)
        {
            RemoveBodyFromList(b);
            print("is not null");
        }
    }


    public void AddBodyToList(Rigidbody rigidbody)
    {
        bodiesToPull.Add(rigidbody);
    }



    public void RemoveBodyFromList(Rigidbody rigidbody)
    {
        bodiesToPull.Remove(rigidbody);
    }

    public void Shoot()
    {
        body.constraints = RigidbodyConstraints.FreezeAll;
        colider.enabled = true;
        box.enabled = false;
        pulling = true;
        print("PULLING");
    }
}
