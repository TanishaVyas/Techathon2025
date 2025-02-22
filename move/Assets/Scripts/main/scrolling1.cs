using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrolling1 : MonoBehaviour
{   
    Material mat;
    MeshRenderer mr;

    public float parralax = 2f;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 offset = mat.mainTextureOffset;
        offset.x = transform.position.x / transform.localScale.x / parralax;
		offset.y = transform.position.y / transform.localScale.y / parralax;

        mat.mainTextureOffset = offset;
        
    }
}
