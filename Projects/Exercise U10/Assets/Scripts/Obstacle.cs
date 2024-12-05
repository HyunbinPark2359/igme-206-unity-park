using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float radius;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        minX = mySpriteRenderer.bounds.min.x;
        maxX = mySpriteRenderer.bounds.max.x;
        minY = mySpriteRenderer.bounds.min.y;
        maxY = mySpriteRenderer.bounds.max.y;
        radius = mySpriteRenderer.bounds.extents.x;
    }
}
