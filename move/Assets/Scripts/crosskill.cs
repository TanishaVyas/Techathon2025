using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosskill : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("fireball"))
        {
            GameObject.FindWithTag("GameController").GetComponent<ScoreManager>().incrementScore(10, transform.position);
            Destroy(gameObject);

        }


    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Heal>().decreaseHelath(10);
        }
}

}