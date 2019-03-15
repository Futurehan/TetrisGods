using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public BlockSpawner PlayerOneSpaner;
    public BlockSpawner PlayerTwoSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       Debug.Log(GameManager.GetDistanceToFloor(PlayerOneSpaner.GetTopMostPoint()));
    }
}
