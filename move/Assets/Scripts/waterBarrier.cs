using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class waterBarrier : MonoBehaviour
{
    public GameObject barrierObject; // Reference to the barrier object
    public float barrierDuration = 5f; // Duration for which the barrier stays on
    public Transform pla;
    private bool isBarrierActive = false;
    
    void Update()
    {   if(pla!=null)
        transform.position= pla.position;
        // Check if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {   pla.GetComponent<colourChangePortal>().water();
            ToggleBarrier();
        }
    }

    void ToggleBarrier()
    {
        if (!isBarrierActive)
        {
            // Turn the barrier object on
            barrierObject.SetActive(true);
            isBarrierActive = true;
            // Start a coroutine to turn off the barrier after a duration
            Invoke("TurnOffBarrierAfterDelay",barrierDuration);
        }
    }

    void TurnOffBarrierAfterDelay()
    {
        

        // Turn the barrier object off
        barrierObject.SetActive(false);
        isBarrierActive = false;
    }
}
