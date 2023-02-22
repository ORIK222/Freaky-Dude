using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageType : MonoBehaviour
{
    public bool IsTrue;
    public Sprite Sprite;

    private void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
