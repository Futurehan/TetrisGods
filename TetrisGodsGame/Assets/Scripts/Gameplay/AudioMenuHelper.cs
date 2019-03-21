using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuHelper : MonoBehaviour
{
    private MixMusic _currentPlayer;
    
    void Start()
    {
        _currentPlayer = FindObjectOfType<MixMusic>();
    }


    public void ChangeMusic(string music)
    {
        if (_currentPlayer == null)
            _currentPlayer = FindObjectOfType<MixMusic>();
        
        switch (music)
        {
            case "Chip":
                _currentPlayer.Chiptune();
            break;
            case "Dance":
                _currentPlayer.DanceDance();
            break;
            case "Classic":
                _currentPlayer.Classic();
            break;
            case "Piano":
                _currentPlayer.Piano();
            break;
        }
    }
}
