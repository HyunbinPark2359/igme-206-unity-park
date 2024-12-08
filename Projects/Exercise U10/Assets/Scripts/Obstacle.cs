using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    public float radius;

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    public float Radius
    {
        get
        {
            return radius;
        }
    }

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        radius = mySpriteRenderer.bounds.extents.x;
    }
}
