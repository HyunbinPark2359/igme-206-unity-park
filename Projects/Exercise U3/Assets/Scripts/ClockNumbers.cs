using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClockNumbers : MonoBehaviour
{
    [SerializeField]
    // Prefab that can be attached through inspector
    private GameObject prefab;

    // Radius of the clock numbers
    private float radius = 2.3f;
    // Store a reference to the prefab being instantiated
    TextMesh textMesh;

    void Start()
    {
        // Create and position all 12 clock numbers in the scene
        for (int i = 1; i < 13; i++)
        {
            // Calculate the angle in radians for trigonometry
            float radians = (360f * i / 12) * Mathf.Deg2Rad;

            // Use trigonometry to find x and y where the clock numbers will be positioned
            Vector3 position = new Vector3(radius * Mathf.Sin(radians), radius * Mathf.Cos(radians), 0);

            // Instantiate instance of the prefab
            GameObject instantiatedPrefab = Instantiate(prefab, position, Quaternion.identity);

            // Save the instantiated prefab in textMesh
            textMesh = instantiatedPrefab.GetComponent<TextMesh>();

            // Change the text
            textMesh.text = i.ToString();
        }
    }
}
