using UnityEngine;

public class RotateLoop : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float rotationAmplitude = 45f; 

    void Update()
    {
        
        float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * rotationAmplitude;

        
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }
}
