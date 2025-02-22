 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterCircle : MonoBehaviour
{
    public GameObject player;
    public float radius= 5f;
    private Renderer rendererComponent;
     public float timerDuration = 1f; // Duration of the timer in seconds
    public float timer = 0f; // Current timer value
    public bool timerRunning = false; // Flag to indicate whether the timer is running
    public groundEnemy gr;
    void Start()
    {
        rendererComponent = GetComponent<Renderer>();
        rendererComponent.material.color = Color.white;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Change the player's color to red
           rendererComponent.material.color= Color.red;
           gr.canshoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset the timer
            rendererComponent.material.color= Color.white;
            timerRunning = false;
            gr.canshoot = false;
        }
    }

    void ffprint(){
        Debug.Log("Timer has ended!");
    }


     void OnDrawGizmosSelected()
    {
        // Draw a wire sphere around the enemy to represent the radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
