using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    public List<GameObject> blockList;
    private float Timer = 2;
    private bool callNext = true;
     [SerializeField]
    public enum playerID {Player_1, Player_2 };
    public playerID player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Timer -=  Time.deltaTime;
        if (Timer <= 0 && callNext)
        {
          //  print(blockList.Capacity - 1);
            GameObject cached = Instantiate(blockList[Random.Range(0,blockList.Capacity)],gameObject.transform.position,Quaternion.identity);
            cached.GetComponent<BlockController>().Activate(this,player);
            Timer = 2;
            callNext = false;
        }
    }
    public void CallNext()
    {
        callNext = true;
    }
}
