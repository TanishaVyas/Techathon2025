using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5;
    [SerializeField] float MaxSpeed = 25;
    [SerializeField] float speedReduction = 0f;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer outlinespriteRenderer;
    [SerializeField] SpriteRenderer dedspriteRenderer;

    public Rigidbody2D rb;
    Quaternion target;

    [SerializeField]
    GameObject earth;
    // Previous movement direction
    private Vector2 prevDir;

    // Gravity pull settings
    public float gravityPullForce = 10f; // Adjust this value as needed
    public float gravityRange = 5f; // Adjust this value as needed

    // Flag to indicate if control key is pressed
    private bool isControlPressed = false;

    // Closest planet reference
    private GameObject closestPlanet;

    // Reference to the ring GameObject in the planet
    private GameObject planetRing;

    // Reference to the currently attached planet
    private GameObject attachedPlanet;

    public ParticleSystem earthpl;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        if (movement != Vector2.zero)
        {
            // Check if direction changed abruptly
            if (Vector2.Dot(movement, prevDir) < 0)
            {
                // Reset velocity when changing direction abruptly
                rb.velocity = Vector2.zero;
            }

            prevDir = movement;

            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            target = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector2 totalForce = movement * speed;

            // Apply gravity pull if control key is pressed and closest planet is chosen
            if (isControlPressed && closestPlanet != null && earth.activeSelf)
            {
                totalForce += CalculateGravityPull();
                earthpl.Play();
            }
            else
            {
                earthpl.Stop();
            }
            rb.AddForce(totalForce, ForceMode2D.Force);

            if (rb.velocity.magnitude > MaxSpeed)
            {
                rb.velocity = rb.velocity.normalized * MaxSpeed;
            }

            // Flip sprite if moving horizontally
            spriteRenderer.flipY = (movement.x < 0);
            outlinespriteRenderer.flipY = (movement.x < 0);
            dedspriteRenderer.flipY = (movement.x < 0);
        }

        // Smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, 0.1f);
    }

    // Calculate gravity pull towards the closest planet
    Vector2 CalculateGravityPull()
    {
        Vector2 totalGravity = Vector2.zero;

        if (closestPlanet != null)
        {
            Vector2 direction = closestPlanet.transform.position - transform.position;
            float distance = direction.magnitude;

            if (distance > 0)
            {
                float gravityStrength = gravityPullForce / distance;
                totalGravity = direction.normalized * gravityStrength * 1.25f;
            }
        }

        return totalGravity;
    }

    // Update closest planet reference when control key is pressed
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameObject.GetComponent<poweruphooser>().powerupelement==2)
        {
            isControlPressed = true;
            FindClosestPlanet();

            // Activate the ring when attached to a planet
            if (attachedPlanet != null && planetRing != null)
            {
                planetRing.SetActive(true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isControlPressed = false;

            // Deactivate the ring when leaving the planet
            if (attachedPlanet != null && planetRing != null)
            {
                planetRing.SetActive(false);
            }
        }
    }

    // Find the closest planet
    void FindClosestPlanet()
    {
        closestPlanet = null;
        float closestDistance = Mathf.Infinity;

        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        foreach (GameObject planet in planets)
        {
            Vector2 direction = planet.transform.position - transform.position;
            float distance = direction.magnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlanet = planet;

                // Update the reference to the ring GameObject
                if (planet.transform.childCount >= 2)
                {
                    planetRing = planet.transform.GetChild(1).gameObject;
                }
            }
        }

        // Update the currently attached planet
        attachedPlanet = closestPlanet;
    }

    public void RedSpeed()
    {
        rb.velocity *= speedReduction;
    }
}
