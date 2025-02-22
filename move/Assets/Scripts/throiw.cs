using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throiw : MonoBehaviour
{   [SerializeField]
    airEnemy ar;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            ar.isInCircle=false;
            ar.ThrowPlayerOut();
        }
    }
}
