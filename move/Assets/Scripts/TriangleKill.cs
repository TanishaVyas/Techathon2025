using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleKill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("fireball"))
        {
            GameObject.FindWithTag("GameController").GetComponent<ScoreManager>().incrementScore(10, transform.position);
            Destroy(gameObject);

        }
}
}
