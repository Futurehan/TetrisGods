
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockSpawner : MonoBehaviour
{
    public delegate void OnSpawn(BlockController boxObject, GameManager.PlayerIndex player);
    public OnSpawn OnSpawner;
    public List<GameObject> blockList;
    public float BlockSpawnWidth;
    public GameManager.PlayerIndex player;  
    private BlockController _currentBlock;

    private int _activeBlockLayer = 10;
    private int _inactiveBlockLayer = 9;

    public Action<GameObject> OnNextBlockShow;
    private GameObject _nextBlock;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        if (GameManager.IsPaused) GameManager.SetupGame();   
        CallNext();
    }

    public void CallNext()
    {
        if (GameManager.IsPaused)
        {
            Debug.Log("Paused");
            return;
        }

        if (_nextBlock == null)
            _nextBlock = blockList[Random.Range(0, blockList.Capacity)];
  

        if (_currentBlock)
        {
            for (int i = 0; i < _currentBlock.transform.childCount; i++)
            {
                _currentBlock.transform.GetChild(i).gameObject.layer = _inactiveBlockLayer;
                foreach (Collider childCollider in _currentBlock.transform.GetChild(i).GetComponentsInChildren<Collider>())
                    childCollider.gameObject.layer = _inactiveBlockLayer;
            }
        }


        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.x += Random.Range(BlockSpawnWidth * -0.5f, BlockSpawnWidth * 0.5f);
        
        GameObject cached = Instantiate(_nextBlock, spawnPos, Quaternion.identity);
        BlockController block = cached.GetComponent<BlockController>();
        block.Activate(this, player);
     
        for (int i = 0; i < block.transform.childCount; i++)
        {
            block.transform.GetChild(i).gameObject.layer = _activeBlockLayer;
            foreach (Collider childCollider in block.transform.GetChild(i).GetComponentsInChildren<Collider>())
                childCollider.gameObject.layer = _activeBlockLayer;


        }
        _currentBlock = block;
        if(OnSpawner != null)
            OnSpawner.Invoke(block, player);
       
        //Setting new block
        _nextBlock = _nextBlock = blockList[Random.Range(0, blockList.Capacity)];
        OnNextBlockShow?.Invoke(_nextBlock);
    }

    public Vector3 GetTopMostPoint()
    {
        RaycastHit hit;
        if (!Physics.BoxCast(transform.position, new Vector3(BlockSpawnWidth * 0.5f, 0), Vector3.down, out hit, Quaternion.identity, 1000 , 1 << _inactiveBlockLayer ))
            return Vector3.negativeInfinity;

//        Debug.Log("Object hit: " + hit.transform.gameObject);
        
       return hit.point;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, new Vector3(BlockSpawnWidth, 0.7f, 0));
    }
#endif
}

