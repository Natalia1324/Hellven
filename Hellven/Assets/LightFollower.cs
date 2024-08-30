using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFollower2D : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController
    public float rotationSpeed = 5f; // Speed at which the light rotates

    private Light2D light2D; // Reference to the Light2D component

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        if (light2D == null)
        {
            UnityEngine.Debug.LogError("No Light2D component found on this GameObject.");
        }
        if (playerController == null)
        {
            UnityEngine.Debug.LogError("PlayerController reference is missing.");
        }
    }

    private void Update()
    {
        if (playerController == null)
            return;

        Vector2 inputDirection = playerController.input;

        if (inputDirection != Vector2.zero)
        {
            // Calculate the angle in degrees between the x-axis and the input direction vector
            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;

            // Smoothly rotate the light to the new angle
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90); // Adjust based on your light setup
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
