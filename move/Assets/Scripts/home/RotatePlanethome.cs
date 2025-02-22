using UnityEngine;

public class room : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        // Rotate the planet around its own axis
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}

