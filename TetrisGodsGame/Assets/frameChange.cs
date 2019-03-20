using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frameChange : MonoBehaviour
{
    public SpriteRenderer activeSprite;

    public void ChangeImage()
    {
        activeSprite = GetComponent<SpriteRenderer>();
        activeSprite.enabled = true;
    }


   
}
