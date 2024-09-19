using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFollower2D : MonoBehaviour
{
    public PlayerController playerController;
    public float rotationSpeed = 5f; 
    public float shiftDistance = 1f;    

    private Light2D light2D;

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
            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90); 
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            Vector3 shift = inputDirection * shiftDistance;
            transform.position = playerController.transform.position + shift;
        }
    }


}
