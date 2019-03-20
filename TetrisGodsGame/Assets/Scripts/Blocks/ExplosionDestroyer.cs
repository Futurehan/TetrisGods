using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        RuntimeAnimatorController ac = animator.runtimeAnimatorController;


        Destroy(gameObject, ac.animationClips[0].length);
    }

    



}
