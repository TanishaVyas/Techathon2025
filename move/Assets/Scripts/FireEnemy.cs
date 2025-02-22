using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public Transform inner;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        inner.Rotate(0, 0, 2f);
        // Check if the player is not null (exists)
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = player.position - transform.position;
            direction.Normalize(); // Normalize to get a unit vector

            // Move the enemy towards the player
            transform.Translate(direction * speed * Time.deltaTime);

        }

    }
   
}