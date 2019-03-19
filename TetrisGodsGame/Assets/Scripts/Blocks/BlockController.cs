using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BlockController : MonoBehaviour
{

 
    public bool Active = false;
    public List<GameObject> subBlocks;
    public float forceAmmount = 900.0f;
    public float thrustAmmount = -100.0f;
    public float spawnTimer = 2.0f;

    private bool countdownBool = false;
    private bool activateSpawn = false;
    private string verticalID;
    private string horizontalID;

    private Rigidbody body;
    private BlockSpawner blockSpawner;
    private GameObject box;
    private GameManager.PlayerIndex owner;
    private float thrustLerpDistance = 0;
    private float thrustLerpSpeed = 15;
    private float leftthrustLerpDistance = 0;
    private float rightthrustLerpDistance = 0;


    public GameObject downThrust;
    public GameObject leftThrust;
    public GameObject rightThrust;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
            Movement();

        if (countdownBool)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                Active = false;
                countdownBool = false;
                Spawn();
            }
        }
    }

    public void Spawn()
    {
        if (downThrust!=null)
        {
            downThrust.SetActive(false);
            leftThrust.SetActive(false);
            rightThrust.SetActive(false);
        }
        blockSpawner.CallNext();
        Active = false;
        activateSpawn = false;
        this.enabled = false;
    }

    private void Movement()
    {
        if (Active)
        {
            Vector3 forceInput = Input.GetAxis(verticalID) * body.transform.up * forceAmmount * Time.deltaTime;
            Vector3 turnInput = Input.GetAxis(horizontalID) * body.transform.forward * thrustAmmount * Time.deltaTime;
            body.AddTorque(turnInput);
            body.AddForce(forceInput);
        }

        //Thrust graphic handler
        #region
        if (Input.GetAxis(verticalID) != 0)
        {
            if (downThrust.transform.localScale.y < 1)
            {
                downThrust.transform.localScale = Vector3.Lerp(new Vector3(1, 0, 1), new Vector3(1, 1, 1), thrustLerpDistance);
                thrustLerpDistance = thrustLerpDistance + thrustLerpSpeed * Time.deltaTime;
            }

        }
        else
        {
            if (downThrust.transform.localScale.y > 0)
            {
                downThrust.transform.localScale = Vector3.Lerp(new Vector3(1, 0, 1), new Vector3(1, 1, 1), thrustLerpDistance);
                thrustLerpDistance = thrustLerpDistance - thrustLerpSpeed * Time.deltaTime;
            }
        }
        if (Input.GetAxis(horizontalID) !=0)
        {
            if (Input.GetAxis(horizontalID) > 0)
            {
                if (leftThrust.transform.localScale.y < 1)
                {
                    leftThrust.transform.localScale = Vector3.Lerp(new Vector3(1, 0, 1), new Vector3(1, 1, 1), leftthrustLerpDistance);
                    leftthrustLerpDistance = leftthrustLerpDistance + thrustLerpSpeed * Time.deltaTime;
                }
            }

            else if (Input.GetAxis(horizontalID) < 0)
            {
                if (rightThrust.transform.localScale.y < 1)
                {
                    rightThrust.transform.localScale = Vector3.Lerp(new Vector3(1, 0, 1), new Vector3(1, 1, 1), rightthrustLerpDistance);
                    rightthrustLerpDistance = rightthrustLerpDistance + thrustLerpSpeed * Time.deltaTime;
                }
            }
        }
        else
        {
            if (leftThrust.transform.localScale.y > 0)
            {
                leftThrust.transform.localScale = Vector3.Lerp(new Vector3(1, 0, 1), new Vector3(1, 1, 1), leftthrustLerpDistance);
                leftthrustLerpDistance = leftthrustLerpDistance - thrustLerpSpeed * Time.deltaTime;
            }
            if (rightThrust.transform.localScale.y > 0)
            {
                rightThrust.transform.localScale = Vector3.Lerp(new Vector3(1, 0, 1), new Vector3(1, 1, 1), rightthrustLerpDistance);
                rightthrustLerpDistance = rightthrustLerpDistance - thrustLerpSpeed * Time.deltaTime;
            }
        }
        #endregion  

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!Active) return;
        if (collision.gameObject.GetComponent<BlockController>() != null && collision.gameObject != gameObject || collision.gameObject.tag == "Ground")
            countdownBool = true;       
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
        spawnTimer = 0.0f;
        countdownBool = true;
        subBlocks.Remove(childObjectHit);
        Destroy(childObjectHit);
        if (OnBoxDestruction != null && subBlocks.Count <= 0)
        {
            OnBoxDestruction.Invoke(this, owner);
            if (!Active && activateSpawn)
            {
                activateSpawn = true;
                Spawn();
            }
        }
    }
}
