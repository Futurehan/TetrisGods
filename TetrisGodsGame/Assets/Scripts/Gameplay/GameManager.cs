using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public enum PlayerIndex
    {
        One,
        Two
    }
    private static GameManager _instanceHelper;
    private static GameManager _instance
    {
        get
        {
            if (_instanceHelper == null)
            {
                _instanceHelper = FindObjectOfType<GameManager>();
                if(_instanceHelper != null) _instanceHelper.Setup(); 
            }
                
                  
            if (_instanceHelper == null)
            {
                _instanceHelper = new GameObject("GameManager").AddComponent<GameManager>();
                _instanceHelper.Setup();
            }
    
            return _instanceHelper;
        }
    }
         
    private BlockLists BlockList;
    private Dictionary<PlayerIndex, int> PlayerScore;
    private bool IsPaused = true;
    private float CurrentTime;
    private GameplaySettings Settings;
    private GameObject _floorTag;
    public static Action RoundOver;


    private void Awake()
    {
        IsPaused = true;
    }


    private void Setup()
    {
        Debug.Log("Set up");
        
        Settings = Instantiate(AssetDatabase.LoadAssetAtPath<GameplaySettings>("Assets/Scriptables/Settings/GameplaySettings.asset"));
        
        PlayerScore = new Dictionary<PlayerIndex, int>();
        
        for (int i = 0; i < (int)PlayerIndex.Two; i++)
        {
            PlayerScore.Add((PlayerIndex)i, 0);
        }
      
        if (BlockList == null) 
            BlockList = FindObjectOfType<BlockLists>();
     
        if (BlockList == null)  
            BlockList = new GameObject("BlockList").AddComponent<BlockLists>();
        
        _floorTag = GameObject.FindWithTag("Floor");
        if (_floorTag == null)
        {
            _floorTag = new GameObject();
            _floorTag.tag = "Floor";
        }
        
  
        IsPaused = false;
    }

    private static void SetupGame()
    {
        _instance.Setup();
    }

    public static void AddScore(PlayerIndex playerIndex, int score)
    {
        _instance.PlayerScore[playerIndex] += score;
    }

    public static int GetScore(PlayerIndex playerIndex)
    {
        return _instance.PlayerScore[playerIndex];
    }

    public static float GetCurrentBlockDropIntveal()
    {
        return (1 - _instance.Settings.BlockSpawnInterval.Evaluate(_instance.CurrentTime / _instance.Settings.RoundTime)) * _instance.Settings.MaxBlockSpawnInterval;
    }

    public static float GetPlayerBlockHeight(PlayerIndex player)
    {
        float height = 0;
        Debug.Log(_instance.BlockList);
        BlockController[] blocks = _instance.BlockList.GetPlayerBlocks(player).ToArray();
        for (int i = 0; i < blocks.Length; i++)
        {
            float dist = _instance._floorTag.transform.position.y - blocks[i].transform.position.y;
            dist = Mathf.Abs(dist);
            if (dist > height)
                height =  dist;
        }
       
        return height;
    }

    void Update()
    {
        if (IsPaused) return;
        
        
        CurrentTime += Time.deltaTime;    
          
        if (CurrentTime > Settings.RoundTime)
        {
            RoundOver?.Invoke();
            Debug.Log("Round over!");
            IsPaused = true;
        }
           
    }
}
