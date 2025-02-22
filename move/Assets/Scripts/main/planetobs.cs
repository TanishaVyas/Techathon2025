using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetobs : MonoBehaviour
{


    public ScoreManager sc;
    public GameObject differentColor;
    public GameObject sameColor;

    void Start()
    {
        float slsize = Random.Range(0.3f, 0.7f);
        transform.localScale = new Vector3(slsize, slsize, 1);
        sc =
            GameObject
                .FindWithTag("GameController")
                .GetComponent<ScoreManager>();
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, 1f);
    }

    // Start is called before the first frame update
    /*void OnBecameInvisible()
    {
        Destroy(gameObject);
    }*/


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (
                other
                    .gameObject
                    .GetComponent<colourChangePortal>()
                    .body
                    .color ==
                transform.GetChild(0).GetComponent<colourPlanet>().body.color
            )
            {
                sc.incrementScore(25,transform.position);
                Instantiate(sameColor,transform.position, Quaternion.identity);
                other.gameObject.GetComponent<colourChangePortal>().samePlanetMusic();
                Destroy (gameObject);
                
            }
            else
            {
                other
                    .gameObject
                    .GetComponent<movement>().RedSpeed();
                Instantiate(differentColor,transform.position, Quaternion.identity);
                other.gameObject.GetComponent<colourChangePortal>().setFlash();
                other.gameObject.GetComponent<colourChangePortal>().setFlash();
                other.gameObject.GetComponent<colourChangePortal>().diffPlanetMusic();
                Destroy (gameObject);
            }
        }
    }
}
