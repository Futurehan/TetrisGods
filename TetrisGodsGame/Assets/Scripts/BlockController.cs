using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    Rigidbody body;
    BlockSpawner blockSpawner;
    public bool Active = false;
    private string verticalID;
    private string horizontalID;
    private KeyCode code;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Active)
        {
            Movement();

        }
    }


    private void Movement()

    {

        Vector3 forceInput = Input.GetAxis(verticalID) * body.transform.up * 900 * Time.deltaTime;
        Vector3 turnInput = Input.GetAxis(horizontalID)*body.transform.forward * -400 *Time.deltaTime;
        body.AddTorque(turnInput);
        body.AddForce(forceInput);
        if (Input.GetKeyUp(code))
        {
            Active = false;
            blockSpawner.CallNext();
        }

    }
    public void Activate(BlockSpawner spawner, BlockSpawner.playerID PlayerID)
    {
        blockSpawner = spawner;
        switch (PlayerID)
        {
            case BlockSpawner.playerID.Player_1:
                verticalID = "Vertical";
                horizontalID = "Horizontal";
                code = KeyCode.Q;
                break;
            case BlockSpawner.playerID.Player_2:
                verticalID = "Verticalp2";
                horizontalID = "Horizontalp2";
                code = KeyCode.Keypad0;
                break;
        }
        Active = true;
    }
    public void Death()
    {
        if (Active == true)
        {
            blockSpawner.CallNext();
            Active = false;
        }
        Destroy(gameObject,2);
    }
}
