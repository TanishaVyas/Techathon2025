using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnemy : MonoBehaviour
{
    public GameObject water;
    public Transform inner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        inner.transform.Rotate(0, 0, 2f);
    }
    public void spawnS()
    {
        Instantiate(water, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
