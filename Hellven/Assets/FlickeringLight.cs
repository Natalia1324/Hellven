using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    public Light2D light2D;

    public float flickerSpeed = 1.0f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.0f;
    public bool flickerColor = false;
    public Color minColor = Color.white; 
    public Color maxColor = Color.white; 

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

        intensityChangeInterval = UnityEngine.Random.Range(0.1f, 0.5f);
        if (flickerColor)
        {
            colorChangeInterval = UnityEngine.Random.Range(0.1f, 0.5f);
        }
    }

    void Update()
    {
        if (light2D == null) return;

        timeCounter += Time.deltaTime * flickerSpeed;

        intensityChangeTimer += Time.deltaTime;
        if (intensityChangeTimer >= intensityChangeInterval)
        {
            intensityChangeTimer = 0.0f;
            intensityChangeInterval = UnityEngine.Random.Range(0.1f, 0.5f);
            float intensity = UnityEngine.Random.Range(minIntensity, maxIntensity);
            light2D.intensity = intensity;
        }

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

    public void ApplyPreset(FlickeringLightPreset preset)
    {
        if (preset.flickerSpeed.HasValue)
            flickerSpeed = preset.flickerSpeed.Value;

        if (preset.minIntensity.HasValue)
            minIntensity = preset.minIntensity.Value;

        if (preset.maxIntensity.HasValue)
            maxIntensity = preset.maxIntensity.Value;

        if (preset.flickerColor.HasValue)
            flickerColor = preset.flickerColor.Value;

        if (preset.minColor.HasValue)
            minColor = preset.minColor.Value;

        if (preset.maxColor.HasValue)
            maxColor = preset.maxColor.Value;
    }
}

