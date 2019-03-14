using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class HighscoreManager : MonoBehaviour
{
    private static string scoreFileName = "Score.txt";

    public static void SaveScore(string name, uint score)
    {
        ScoreStruct newScore = new ScoreStruct { Name = name, Score =  score};
        List<ScoreStruct> CurrentScore = new List<ScoreStruct>();
        if (System.IO.File.Exists(Application.dataPath + scoreFileName))
            CurrentScore = JsonUtility.FromJson<List<ScoreStruct>>(Application.dataPath + scoreFileName);

        CurrentScore.Add(newScore);        
        CurrentScore.OrderByDescending(value => value.Score);
        
        if(CurrentScore.Count > 10)
            CurrentScore.RemoveRange(10, CurrentScore.Count - 11);

    }
    
}
[Serializable]
public struct ScoreStruct
{
    public string Name;
    public uint Score;
}

