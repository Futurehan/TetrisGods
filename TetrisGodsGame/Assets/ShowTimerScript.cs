using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTimerScript : MonoBehaviour
{
    private TextMeshPro ourTimer;
    

    // Start is called before the first frame update
    void Start()
    {
        ourTimer = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeLeft = GameManager.MaxTime - GameManager.RoundTime;

        ourTimer.text = ((int) timeLeft).ToString();

    }
}
