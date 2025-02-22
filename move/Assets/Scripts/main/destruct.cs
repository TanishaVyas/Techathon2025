using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruct : MonoBehaviour
{
    public float destroytime;
    private float timer;
    Vector3 sc = Vector3.one;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale=new Vector3(0.1f,0.1f,0.1f);
        timer = destroytime;
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(transform.localScale.x<=0.5f)
        transform.localScale += sc*Time.deltaTime*2;
        timer-= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);

        }
        
    }
}
