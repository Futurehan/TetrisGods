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
   
    private AudioSource _winSound;
 

    // public Image 

    // Start is called before the first frame update
    private void OnEnable()
    {
        GameManager.RoundOver += ShowVictoryScreen;
        _winSound = GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePausePanel();
    }

    private void ShowVictoryScreen(GameManager.PlayerIndex winner)
    {
        VictoryImage.sprite = winner == GameManager.PlayerIndex.One ? playerOneImage : playerTwoImage;
        _winSound.Play();
        VictoryPanel.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void TogglePausePanel()
    {
        PausePanel.SetActive(!PausePanel.activeSelf);

        GameManager.IsPaused = PausePanel.activeSelf;
    }

    private void OnDisable()
    {
        GameManager.RoundOver -= ShowVictoryScreen;
    }
}
