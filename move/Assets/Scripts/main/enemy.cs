using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;

    public GameObject Player;

    public float speed;

    public Vector3 velocity;
    public GameObject differentColor;
    public GameObject sameColor;
    public float score_threshold = 10;
    public float currentSpeed;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    public ScoreManager sc;
    [SerializeField] GameObject end;

    void Start()
    {
    }

    // Update is called once per frame
   void FixedUpdate()
{
    Vector2 direction = (player.position - transform.position).normalized;

    // Increase speed based on the score threshold
    int scoreThreshold = Mathf.FloorToInt(sc.score / score_threshold);
    currentSpeed = speed + scoreThreshold * 0.2f; // Adjust the multiplier as needed

    // Move the enemy using Rigidbody2D methods
    rb.AddForce(direction * currentSpeed * Time.fixedDeltaTime,ForceMode2D.Impulse);

    // Rotate the enemy to face the direction of movement
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);

    // Flip sprite if moving horizontally
    spriteRenderer.flipY = (direction.x < 0);
}



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //add function call to end player effect here
            other.gameObject.GetComponent<colourChangePortal>().endMusic();
            end.SetActive(true);
            Destroy(other.gameObject);
            Destroy(gameObject);
            sc.setHighScore();
            sc.gameObject.SetActive(false);
        }
        else if (other.tag == "Planet")
        {
            if (
                other
                    .gameObject
                    .transform
                    .GetChild(0)
                    .GetComponent<colourPlanet>()
                    .body
                    .color !=
                Player.GetComponent<colourChangePortal>().body.color
            )
            {
               sc.incrementScore(35, other.gameObject.transform.position);
                Player.GetComponent<colourChangePortal>().samePlanetMusic();
                Instantiate(differentColor,other.gameObject.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                //score plus
            }
            else
            {
                sc.incrementScore(-10,other.gameObject.transform.position);
                Instantiate(sameColor,other.gameObject.transform.position, Quaternion.identity);
                 Player.GetComponent<colourChangePortal>().diffPlanetMusic();
                Destroy(other.gameObject);
            }
        }
    }
 void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.tag == "Player")
        {
            //add function call to end player effect here
            other.gameObject.GetComponent<colourChangePortal>().endMusic();
            end.SetActive(true);
            Destroy(other.gameObject);
            Destroy(gameObject);
            sc.setHighScore();
            sc.gameObject.SetActive(false);
        }
}
public void stopgame()
{
    //add function call to end player effect here
            Player.gameObject.GetComponent<colourChangePortal>().endMusic();
            end.SetActive(true);
            Destroy(Player.gameObject);
            Destroy(gameObject);
            sc.setHighScore();
            sc.gameObject.SetActive(false);
}
}
