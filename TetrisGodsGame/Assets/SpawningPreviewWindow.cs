using System;
using UnityEngine;
using System.Linq;

public class SpawningPreviewWindow : MonoBehaviour
{
    [Serializable]
    private struct BlockImagePair
    {
        public GameObject Block;
        public Sprite Sprite;
    }

    [SerializeField] private BlockSpawner _blockSpawner;
    [SerializeField] private SpriteRenderer _previewSprite;
    [SerializeField] private BlockImagePair[] _blocks;
    
        
    // Start is called before the first frame update
    void Awake()
    {
        _blockSpawner.OnNextBlockShow += ShowNextBlock;
    }

    private void ShowNextBlock(GameObject obj)
    {
        _previewSprite.sprite = _blocks.ToList().FirstOrDefault(pair => pair.Block == obj).Sprite;
    }

}


