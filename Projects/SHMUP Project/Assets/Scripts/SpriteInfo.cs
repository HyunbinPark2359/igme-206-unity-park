using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Properties to get various vectors of sprite
    public float MinX
    {
        get
        {
            return mySpriteRenderer.bounds.min.x;
        }
    }
    public float MaxX
    {
        get
        {
            return mySpriteRenderer.bounds.max.x;
        }
    }
    public float MinY
    {
        get
        {
            return mySpriteRenderer.bounds.min.y;
        }
    }
    public float MaxY
    {
        get
        {
            return mySpriteRenderer.bounds.max.y;
        }
    }

    // Property to get the radius of sprite
    // Shorter one between x and y extents
    public float Radius
    {
        get
        {
            if (mySpriteRenderer.bounds.extents.x < mySpriteRenderer.bounds.extents.y)
            {
                return mySpriteRenderer.bounds.extents.x;
            }
            else
            {
                return mySpriteRenderer.bounds.extents.y;
            }
        }
    }
}
