using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameManager.PlayerIndex _player;
    [SerializeField] private TextMeshPro _heightText;

    private BlockSpawner _playerBlockSpawner;

    private void Start()
    {
        _playerBlockSpawner = FindObjectsOfType<BlockSpawner>().First(spawner => spawner.player == _player);
    }


    private void Update()
    {
        float height = GameManager.GetDistanceToFloor(_playerBlockSpawner.GetTopMostPoint());
        height = Mathf.Round(height * 100) / 100;    
        _heightText.text = "" + (float.IsInfinity(height) ? 0 : height);
    }
}
