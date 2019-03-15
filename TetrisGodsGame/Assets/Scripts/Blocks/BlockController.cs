using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BlockController : MonoBehaviour
{

 
    public bool Active = false;
    public float spawnTime = 2.0f;
    public List<GameObject> subBlocks;

    private bool activateSpawn = false;
    private string verticalID;
    private string horizontalID;

    private Rigidbody body;
    private BlockSpawner blockSpawner;
    private GameObject box;
    private GameManager.PlayerIndex owner;

    //Delegates
    public delegate void OnDestruction(BlockController boxObject, GameManager.PlayerIndex player);
    public OnDestruction OnBoxDestruction;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentsInChildren<Collider>().ToList().ForEach(child => subBlocks.Add(child.gameObject));

        if (subBlocks.Count <= 0)
            Debug.LogError("the block controller has now sub-blocks assigned");

        body = GetComponent<Rigidbody>();
        box = GetComponent<GameObject>();
        InvokeRepeating("SpawnTimer", 0.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
            Movement(); 
    }

    void SpawnTimer()
    {
        if (activateSpawn)
        {
            blockSpawner.CallNext();
            Active = false;
            activateSpawn = false;
        }
    }

    private void Movement()
    {
        Vector3 forceInput = Input.GetAxis(verticalID) * body.transform.up * 900 * Time.deltaTime;
        Vector3 turnInput = Input.GetAxis(horizontalID)*body.transform.forward * -100 *Time.deltaTime;
        body.AddTorque(turnInput);
        body.AddForce(forceInput);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!Active) return;
        if (collision.gameObject.GetComponent<BlockController>() != null && collision.gameObject != gameObject || collision.gameObject.tag == "Ground")
            activateSpawn = true;
    }

    public void Activate(BlockSpawner spawner, GameManager.PlayerIndex ownerId)
    {
        blockSpawner = spawner;
        owner = ownerId;
        switch (ownerId)
        {
            case GameManager.PlayerIndex.One:
                verticalID = "Vertical";
                horizontalID = "Horizontal";
                break;
            case GameManager.PlayerIndex.Two:
                verticalID = "Verticalp2";
                horizontalID = "Horizontalp2";
                break;
        }
        Active = true;
    }

    public void Death(GameObject childObjectHit)
    {
        if (Active && !activateSpawn)
        {
            Active = false;
            activateSpawn = true;
        }

        subBlocks.Remove(childObjectHit);
        Destroy(childObjectHit);
        if(OnBoxDestruction != null && subBlocks.Count <= 0)
        {
            OnBoxDestruction.Invoke(this, owner);
            if (Active || activateSpawn)
            {
                activateSpawn = true;
                SpawnTimer();
            }
     
        }
           
    }
}
