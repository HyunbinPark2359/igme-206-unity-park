using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHand : MonoBehaviour
{
    private float turnAmount = -6f;

    [SerializeField]
    private bool useDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!useDeltaTime)
        {
            transform.Rotate(0, 0, turnAmount);
        }
        else
        {
            transform.Rotate(0, 0, turnAmount * Time.deltaTime);
        }
    }
}
