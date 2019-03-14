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
                _instanceHelper.Setup();
            }
            if (_instanceHelper == null)
            {
                _instanceHelper = new GameObject("GameManager").AddComponent<GameManager>();
                _instanceHelper.Setup();
            }
            
            return _instanceHelper;
        }
    }
         
    private Dictionary<PlayerIndex, int> PlayerScore;
    private bool IsPaused;
    private float CurrentTime;
    private GameplaySettings Settings;

    public static Action RoundOver;
    
    private void Awake()
    {
        if(Settings != null) return;

        Setup();
    }

    private void Setup()
    {
        Settings = Instantiate(AssetDatabase.LoadAssetAtPath<GameplaySettings>("Assets/Scriptables/Settings/GameplaySettings.asset"));
        
        PlayerScore = new Dictionary<PlayerIndex, int>();
        
        for (int i = 0; i < (int)PlayerIndex.Two; i++)
        {
            PlayerScore.Add((PlayerIndex)i, 0);
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

    void Update()
    {
        if(!IsPaused)
            CurrentTime += Time.deltaTime;    

        if (CurrentTime > Settings.RoundTime)
        {
            RoundOver.Invoke();
        }
            Debug.Log("Round over!");
    }
}
