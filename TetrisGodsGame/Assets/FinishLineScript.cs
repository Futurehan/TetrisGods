using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScript : MonoBehaviour
{

    private float distanceToGoal;
    public BlockSpawner player1;
    public BlockSpawner player2;
    private GameManager.PlayerIndex _winner;
    // Start is called before the first frame update
    void Start()
    {
        distanceToGoal = GameManager.GetDistanceToFloor(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsPaused) return;

        if (GameManager.GetDistanceToFloor(player1.GetTopMostPoint()) > distanceToGoal)
        {
            _winner = GameManager.PlayerIndex.One;
            GameManager.CompleteGame(_winner);
       
            print("Spelare 1 är bäst");
        }

        if (GameManager.GetDistanceToFloor(player2.GetTopMostPoint()) > distanceToGoal)
        {
            _winner = GameManager.PlayerIndex.Two;
            GameManager.CompleteGame(_winner);
            print("Spelare 2 är bäst");
        }
    }
}
