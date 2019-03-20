using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    public GameObject VictoryPanel;
    public GameObject PausePanel;
    public Image VictoryImage;

    [Header("Images")]
    public Sprite playerOneImage;
    public Sprite playerTwoImage;

 

    // public Image 

    // Start is called before the first frame update
    void Start()
    {
        GameManager.RoundOver += ShowVictoryScreen;
    }

    private void ShowVictoryScreen(GameManager.PlayerIndex winner)
    {
        VictoryImage.sprite = winner == GameManager.PlayerIndex.One ? playerOneImage : playerTwoImage;

        VictoryPanel.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void ShowPausePanel()
    {
   //     if()
    }
}
