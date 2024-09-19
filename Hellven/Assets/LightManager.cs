using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public struct FlickeringLightPreset
{
    public float? flickerSpeed; 
    public float? minIntensity;
    public float? maxIntensity;
    public bool? flickerColor;
    public Color? minColor;
    public Color? maxColor;
}

public class LightManager : MonoBehaviour
{ 
    public Dictionary<string, FlickeringLightPreset> flickeringLightPresets = new Dictionary<string, FlickeringLightPreset>();

    void Start()
    {
        // Example presets with some nullable fields
        flickeringLightPresets.Add("BigBlueCrystals", new FlickeringLightPreset
        {
            flickerSpeed = 6.0f,
            minIntensity = 12f,
            maxIntensity = 14f,
            flickerColor = true,
            minColor = new Color(0.1409754f, 0.3104632f, 0.9056604f),
            maxColor = new Color(0.2516465f, 0.5247815f, 0.8207547f),
        });
        flickeringLightPresets.Add("BigPurpleCrystals", new FlickeringLightPreset
        {
            flickerSpeed = 6.0f,
            minIntensity = 12f,
            maxIntensity = 14f,
            flickerColor = true,
            minColor = new Color(0.6957726f, 0.1564169f, 0.8962264f),
            maxColor = new Color(0.9339623f, 0.1806248f, 0.7324539f),
        });

        flickeringLightPresets.Add("SmallBlueCrystals", new FlickeringLightPreset
        {
            flickerSpeed = 8.0f,
            minIntensity = 6f,
            maxIntensity = 10f,
            flickerColor = true,
            minColor = new Color(0.1409754f, 0.3104632f, 0.9056604f),
            maxColor = new Color(0.2516465f, 0.5247815f, 0.8207547f),
        });

        flickeringLightPresets.Add("torchLights", new FlickeringLightPreset
        {
            flickerSpeed = 6.0f,
            minIntensity = 12f,
            maxIntensity = 14f,
            flickerColor = true,
            minColor = new Color(0.972549f, 0.2826378f, 0.1411764f),
            maxColor = new Color(0.8490566f, 0.3848377f, 0.06007475f),
        });

        flickeringLightPresets.Add("SmallRedCrystals", new FlickeringLightPreset
        {
            flickerSpeed = 6.0f,
            minIntensity = 6f,
            maxIntensity = 10f,
            flickerColor = true,
            minColor = new Color(0.8392157f, 0.1058823f, 0.158212f),
            maxColor = new Color(0.9245283f, 0.4052734f, 0.09158063f),
        });

        flickeringLightPresets.Add("Lava", new FlickeringLightPreset
        {
            flickerSpeed = 6.0f,
            minIntensity = 1f,
            maxIntensity = 3f,
            flickerColor = true,
            minColor = new Color(0.8207547f, 0.2064715f, 0.0967871f),
            maxColor = new Color(0.9339623f, 0.3741333f, 0.0572713f),
        });

        flickeringLightPresets.Add("Water", new FlickeringLightPreset
        {
            flickerSpeed = 1.0f,
            minIntensity = 2f,
            maxIntensity = 4f,
            flickerColor = true,
            minColor = new Color(0.2811944f, 0.4756413f, 0.8396226f),
            maxColor = new Color(0.1187255f, 0.7318592f, 0.8679245f),
        });
        flickeringLightPresets.Add("Ice", new FlickeringLightPreset
        {
            flickerSpeed = 1.0f,
            minIntensity = 2f,
            maxIntensity = 4f,
            flickerColor = true,
            minColor = new Color(0.4607956f, 0.7537373f, 0.8962264f),
            maxColor = new Color(0.3887505f, 0.8699166f, 0.9056604f),
        });
        flickeringLightPresets.Add("Candle", new FlickeringLightPreset
        {
            flickerSpeed = 1.0f,
            minIntensity = 2f,
            maxIntensity = 4f,
            flickerColor = true,
            minColor = new Color(0.8962264f, 0.5923514f, 0.08877714f),
            maxColor = new Color(0.9137256f, 0.2235294f, 0.03921569f),
        });

        ApplyPresetsToAll();
    }

    // Apply presets to all child lights of LightManager
    public void ApplyPresetsToAll()
    {
        // Loop through each child group under LightManager
        foreach (Transform group in transform)
        {
            string groupName = group.name;

            // Check if a preset exists for this group
            if (flickeringLightPresets.ContainsKey(groupName))
            {
                FlickeringLightPreset preset = flickeringLightPresets[groupName];

                // Loop through each light in the group
                foreach (Transform lightTransform in group)
                {
                    FlickeringLight flickeringLight = lightTransform.GetComponent<FlickeringLight>();
                    if (flickeringLight != null)
                    {
                        flickeringLight.ApplyPreset(preset);
                    }
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning($"No preset found for group {groupName}");
            }
        }
    }
}