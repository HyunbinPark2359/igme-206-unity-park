using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHand : MonoBehaviour
{
    private float turnAmount = 360;
    public bool useDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!useDeltaTime)
        {
            Quaternion totalRotation = Quaternion.Euler(0, 0, turnAmount);
            transform.rotation = totalRotation;
        }
        else
        {
            Quaternion totalRotation = Quaternion.Euler(0, 0, turnAmount * Time.deltaTime);
            transform.rotation = totalRotation;
        }
    }
}
