using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    public Light2D light2D; // Reference to the Light2D component
    public float flickerSpeed = 1.0f; // Speed of flickering (affects the rate of change)
    public float minIntensity = 0.5f; // Minimum intensity of the light
    public float maxIntensity = 1.0f; // Maximum intensity of the light
    public bool flickerColor = false; // Toggle color flickering
    public Color minColor = Color.white; // Minimum color for flickering
    public Color maxColor = Color.white; // Maximum color for flickering

    private float timeCounter = 0.0f;
    private float intensityChangeTimer = 0.0f;
    private float colorChangeTimer = 0.0f;
    private float intensityChangeInterval;
    private float colorChangeInterval;

    void Start()
    {
        if (light2D == null)
        {
            light2D = GetComponent<Light2D>();
        }


        // Initialize change intervals
        intensityChangeInterval = UnityEngine.Random.Range(0.1f, 0.5f);
        if (flickerColor)
        {
            colorChangeInterval = UnityEngine.Random.Range(0.1f, 0.5f);
        }
    }

    void Update()
    {
        if (light2D == null) return;

        // Update time counter based on flicker speed
        timeCounter += Time.deltaTime * flickerSpeed;

        // Randomly update intensity
        intensityChangeTimer += Time.deltaTime;
        if (intensityChangeTimer >= intensityChangeInterval)
        {
            intensityChangeTimer = 0.0f;
            intensityChangeInterval = UnityEngine.Random.Range(0.1f, 0.5f);
            float intensity = UnityEngine.Random.Range(minIntensity, maxIntensity);
            light2D.intensity = intensity;
        }

        // Randomly update color if enabled
        if (flickerColor)
        {
            colorChangeTimer += Time.deltaTime;
            if (colorChangeTimer >= colorChangeInterval)
            {
                colorChangeTimer = 0.0f;
                colorChangeInterval = UnityEngine.Random.Range(0.1f, 0.5f);
                Color color = new Color(
                    UnityEngine.Random.Range(minColor.r, maxColor.r),
                    UnityEngine.Random.Range(minColor.g, maxColor.g),
                    UnityEngine.Random.Range(minColor.b, maxColor.b),
                    UnityEngine.Random.Range(minColor.a, maxColor.a)
                );
                light2D.color = color;
            }
        }
    }
}
