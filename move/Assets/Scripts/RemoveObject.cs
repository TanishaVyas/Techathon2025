using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObject : MonoBehaviour
{

    public float offScreenTimeThreshold = 2f; // Time in seconds the planet should be off-screen continuously to be deleted

    private float offScreenTimer = 0f;

    private bool isOffScreen = false;

    // Start is called before the first frame update
    private void Update()
    {
        // Check if the planet is off-screen
        if (!IsVisible())
        {
            isOffScreen = true;
            offScreenTimer += Time.deltaTime;

            // If off-screen for more than offScreenTimeThreshold, delete the planet
            if (offScreenTimer >= offScreenTimeThreshold)
            {
                Destroy (gameObject);
            }
        }
        else
        {
            isOffScreen = false;
            offScreenTimer = 0f;
        }
    }

    private bool IsVisible()
    {
        // Check if the planet is visible on the screen
        Vector3 screenPoint =
            Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x > 0 &&
        screenPoint.x < 1 &&
        screenPoint.y > 0 &&
        screenPoint.y < 1 &&
        screenPoint.z > 0;
    }
}
