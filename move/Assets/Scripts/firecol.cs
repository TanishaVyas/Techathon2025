using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firecol : MonoBehaviour
{  
    
    void OnCollisionEnter2D(Collision2D other)
{
    if (other != null) // Null check
    {
        Debug.Log(other.gameObject.name);
        
        if (other.gameObject.CompareTag("Player"))
        {   
            other.gameObject.GetComponent<Heal>().decreaseHelath(10);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("abber"))
        {
            GameObject.FindWithTag("GameController").GetComponent<ScoreManager>().incrementScore(10, transform.position);
            Destroy(gameObject); 
        }
    }


}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Planet"))
        {
            Destroy(gameObject);
        }
    }
}
