using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class RoundTimeSetter : MonoBehaviour
{
    Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = GameManager.RoundTime;
        _slider.onValueChanged.AddListener(delegate { UpdateRoundTimer(); });
    }

    private void UpdateRoundTimer()
    {
        GameManager.EndlessMode = _slider.value <= 0;

        if (_slider.value > 0)
            GameManager.RoundTime = _slider.value;
        else   
            GameManager.RoundTime = float.PositiveInfinity;
        
        
    }

}

