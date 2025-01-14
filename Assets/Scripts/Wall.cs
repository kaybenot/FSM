using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Bounds Bounds { get; private set; }
    
    private void Awake()
    {
        Bounds = GetComponent<BoxCollider2D>().bounds;
    }
}
