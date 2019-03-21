using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq; 

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
         
    public static Action<PlayerIndex> RoundOver;
//    public static Action 
    
    private BlockLists BlockList;
    private Dictionary<PlayerIndex, int> PlayerScore;
    public static bool IsPaused { get; private set; }
    private float CurrentTime;
    private GameplaySettings Settings;
    private GameObject _floorTag;

    public static float RoundTime => _instance.CurrentTime;
    public static float MaxTime => _instance.Settings.RoundTime;

    private BlockSpawner PlayerOneSpawner => FindObjectsOfType<BlockSpawner>().FirstOrDefault(spawner => spawner.player == PlayerIndex.One);
    private BlockSpawner PlayerTwoSpawner => FindObjectsOfType<BlockSpawner>().FirstOrDefault(spawner => spawner.player == PlayerIndex.Two);

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

        CurrentTime = 0;
        IsPaused = false;
    }

    public static void SetupGame()
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

    public static BlockSpawner GetPlayerSpawner(PlayerIndex player)
    {
        if (player == PlayerIndex.One)
            return _instance.PlayerOneSpawner;

        if (player == PlayerIndex.Two)
            return _instance.PlayerTwoSpawner;

        return null;
    }

    public static float GetDistanceToFloor(Vector3 point)
    {
        return float.IsNegativeInfinity(point.y) ? 0 : Mathf.Abs(_instance._floorTag.transform.position.y - point.y);
    }

    public static void CompleteGame(PlayerIndex winner)
    {
        RoundOver?.Invoke(winner);
        IsPaused = true;
    }

    void Update()
    {
        if (IsPaused) return;
        
        
        CurrentTime += Time.deltaTime;    
          
        if (CurrentTime > Settings.RoundTime)
        {
            PlayerIndex winner = PlayerIndex.Noll;
            if (GetPlayerSpawner(PlayerIndex.One).GetTopMostPoint().y > GetPlayerSpawner(PlayerIndex.Two).GetTopMostPoint().y)
                winner = PlayerIndex.One;
            else
                winner = PlayerIndex.Two;

                RoundOver?.Invoke(winner);
            Debug.Log("Round over!");
            IsPaused = true;
        }
           
    }
}
