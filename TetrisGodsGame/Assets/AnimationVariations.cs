using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class AnimationVariations : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] [Range(0, 10)] private float _delay;

    private bool _triggerd;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _triggerd = false;
       // _animator.StopPlayback();
       _animator.StartPlayback();
    }

    private void Update()
    {
        if(_triggerd) return;
        
        _delay -= Time.deltaTime;
        if(_delay <= 0)
            TriggerAnimations();
    }

    private void TriggerAnimations()
    {
        _triggerd = true;
        _animator.StopPlayback();
    }
}
