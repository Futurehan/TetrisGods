using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public delegate void OnSpawn(BlockController boxObject, GameManager.PlayerIndex player);
    public OnSpawn OnSpawner;
    public List<GameObject> blockList;
     [SerializeField]
    public GameManager.PlayerIndex player;

    // Start is called before the first frame update
    void Start()
    {
        CallNext();
    }

    public void CallNext()
    {
        GameObject cached = Instantiate(blockList[Random.Range(0, blockList.Capacity)], gameObject.transform.position, Quaternion.identity);
        BlockController block = cached.GetComponent<BlockController>();
        block.Activate(this, player);
        if(OnSpawner != null)
            OnSpawner.Invoke(block, player);
    }
}
