using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundbullet : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<Heal>().decreaseHelath(10);
            Destroy(gameObject);
        }
    }
}
