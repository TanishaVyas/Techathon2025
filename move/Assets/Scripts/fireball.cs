using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public bool hasshot =false;

    public float shrinkSpeed = 0.05f;
    public GameObject whoshot;

    void Update()
    {   
        transform.Rotate(0, 0, 1f);

        if (!hasshot)
        {
            transform.localScale +=
                new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed) *
                Time.deltaTime;
            if (transform.localScale.x >= 1f)
            {
                hasshot = true;
                whoshot.GetComponent<ShootingAbilities>().StopShooting();
            }
        }
        else
        {
            transform.localScale -=
                new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed) *
                Time.deltaTime;
            if (transform.localScale.x <= 0f)
            {
                Destroy (gameObject);
            }
        }
    }
}
