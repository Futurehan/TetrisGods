using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLists : MonoBehaviour
{
    public List<BlockController> spawnedBlocks = new List<BlockController>();
    private BlockSpawner[] blockSpawnerArray;
    private Dictionary<GameManager.PlayerIndex, List<BlockController>> PlayerBlocks;
    private BlockController blockController;
    // Start is called before the first frame update
    void Awake()
    {
        blockSpawnerArray = FindObjectsOfType<BlockSpawner>();
        blockController = FindObjectOfType<BlockController>();

        for (int i = 0; i < blockSpawnerArray.Length; i++)
            blockSpawnerArray[i].OnSpawner += AddSpawnedBlocks;

        PlayerBlocks = new Dictionary<GameManager.PlayerIndex, List<BlockController>>();
        
        for (int i = 0; i < (int)GameManager.PlayerIndex.Two + 1; i++)
                PlayerBlocks.Add((GameManager.PlayerIndex)i, new List<BlockController>());
        
    }

    public void AddSpawnedBlocks(BlockController block, GameManager.PlayerIndex player)
    {
        if (block != null)
        {
            spawnedBlocks.Add(block);
           // Debug.Log(PlayerBlocks.Count);
            PlayerBlocks[player].Add(block);
            block.OnBoxDestruction += RemovedBlock;
        }
    }
    
    public void RemovedBlock(BlockController block, GameManager.PlayerIndex player)
    {
        if (spawnedBlocks.Contains(block))
        {
            spawnedBlocks.Remove(block);
            PlayerBlocks[player].Remove(block);
            Destroy(block.gameObject);
        }
    }

    public List<BlockController> GetPlayerBlocks(GameManager.PlayerIndex player)
    {
        return PlayerBlocks[player];
    }
}
