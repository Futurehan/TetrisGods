using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameplaySettings", menuName = "Gameplay/Settings", order = 1)]
public class GameplaySettings : ScriptableObject
{
    public float RoundTime;
    public AnimationCurve BlockSpawnInterval;
    public float VictoryHeight;
    public int NumberOfLives;

}
