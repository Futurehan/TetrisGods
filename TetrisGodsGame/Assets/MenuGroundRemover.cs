using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGroundRemover : MonoBehaviour
{
    public float RemoveGroundTime = 120f;
    public bool ShouldReset;
    private float _timer;
    private Animator _groundAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _timer = RemoveGroundTime;
        _groundAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShouldReset) return;

        if(_timer <= 0)
        {
            _groundAnimator.SetTrigger("Reset");
            _timer = RemoveGroundTime;
        }
        else
            _timer -= Time.deltaTime;
    }

 
}
