using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BlockSpawner _blockSpawner;

    [SerializeField] private float _reactionDelays;
    [SerializeField] private float _significantHeightDiff;
    [SerializeField] private float _sampleTime = 1f;
 

private float heigtDiffTheLastSec;
    private float currentHeight;

    private float _reactionTimer;

    public List<(float time, Vector3 pos)> diffList = new List<(float time, Vector3 pos)>(); 
    
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if (_reactionTimer > 0)
        {
            _reactionTimer -= Time.deltaTime;
            return;
        }

        Vector3 topPoint = _blockSpawner.GetTopMostPoint();
        if (float.IsNegativeInfinity(topPoint.y))
            topPoint = Vector3.one * -10;
        
        diffList.Add((Time.time, topPoint));

        int index = 0;
         
        for (int i = diffList.Count - 1; i >= 0 ; i--)
        {
            if (Time.time - diffList[i].time > _sampleTime)
                diffList.RemoveAt(i);
            else
                index = i;
        }

        if(diffList.Count <= index) return;
        
        float heightDiff = topPoint.y - diffList[index].pos.y;


        
        if (Mathf.Abs(heightDiff) > _significantHeightDiff)
        {
            bool happy = heightDiff > 0;
            Debug.Log("Diff!");
            _animator.SetTrigger(happy ? "Yell" : "Sad");
            _reactionTimer = _reactionDelays;
        }
   
            
        
    }
}
