using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5.0f;
    protected Vector3 objectPosition;
    protected Vector3 velocity;
    protected Vector3 bulletDirection;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        objectPosition = transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Move the bullet
        objectPosition = transform.position;
        velocity = bulletDirection * moveSpeed * Time.deltaTime;
        objectPosition += velocity;
        transform.position = objectPosition;
    }
}
