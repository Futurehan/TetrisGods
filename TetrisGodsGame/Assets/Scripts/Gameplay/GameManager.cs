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
        Two,
        Noll
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
         
    public static Action RoundOver;
//    public static Action 
    
    private BlockLists BlockList;
    private Dictionary<PlayerIndex, int> PlayerScore;
    public bool IsPaused { get; private set; }
    private float CurrentTime;
    private GameplaySettings Settings;
    private GameObject _floorTag;



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

    public static float GetCurrentBlockDropInterval()
    {
        return (1 - _instance.Settings.BlockSpawnInterval.Evaluate(_instance.CurrentTime / _instance.Settings.RoundTime)) * _instance.Settings.MaxBlockSpawnInterval;
    }

    public static BlockController GetPlayerTopBlock(PlayerIndex player)
    {
        BlockController[] blocks = _instance.BlockList.GetPlayerBlocks(player).ToArray();
        if (blocks.Length <= 0) return null;
        float height = 0;
        int index = 0;
        for (int i = 0; i < blocks.Length; i++)
        {
            float dist = _instance._floorTag.transform.position.y - blocks[i].transform.position.y;
            dist = Mathf.Abs(dist);
            if (dist > height)
            {
                index = i;
                height =  dist;
            }
       
        }
       
        return blocks[index];
    }

    public static float GetDistanceToFloor(Vector3 point)
    {
        return Mathf.Abs(_instance._floorTag.transform.position.y - point.y);
    }

    public static float GetCurrentRoundTime()
    {
        return _instance.CurrentTime;
    }

    public static float GetRoundMaxTime()
    {
        return _instance.Settings.RoundTime;
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
