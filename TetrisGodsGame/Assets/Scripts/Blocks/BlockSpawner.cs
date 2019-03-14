using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public delegate void OnSpawn(BlockController boxObject);
    public OnSpawn OnSpawner;
    public List<GameObject> blockList;
     [SerializeField]
    public enum playerID {Player_1, Player_2 };
    public playerID player;

    // Start is called before the first frame update
    void Start()
    {
        CallNext();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void CallNext()
    {
        GameObject cached = Instantiate(blockList[Random.Range(0, blockList.Capacity)], gameObject.transform.position, Quaternion.identity);
        BlockController block = cached.GetComponent<BlockController>();
        block.Activate(this, player);
        if(OnSpawner != null)
            OnSpawner.Invoke(block);
    }
}
