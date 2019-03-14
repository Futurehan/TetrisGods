using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLists : MonoBehaviour
{
    public List<BlockController> spawnedBlocks = new List<BlockController>();
    BlockSpawner[] blockSpawnerArray;
    BlockController blockController;
    // Start is called before the first frame update
    void Awake()
    {
        blockSpawnerArray = FindObjectsOfType<BlockSpawner>();
        blockController = FindObjectOfType<BlockController>();

        for (int i = 0; i < blockSpawnerArray.Length; i++)
            blockSpawnerArray[i].OnSpawner += AddSpawnedBlocks;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddSpawnedBlocks(BlockController blocks)
    {
        if (blocks != null)
        {
            spawnedBlocks.Add(blocks);
            blocks.OnBoxDestruction += RemovedBlock;
        }
    }

    public void RemovedBlock(BlockController blocks)
    {
        if (spawnedBlocks.Contains(blocks))
        {
            spawnedBlocks.Remove(blocks);
            Destroy(blocks.gameObject);
        }
    }
}
