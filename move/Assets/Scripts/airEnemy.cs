using UnityEngine;

public class airEnemy : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation around the circle
    public float scaleSpeed = 0.1f; // Speed of scaling down the player
    public float minScale = 0.1f; // Minimum scale of the player before being thrown out
    public float exitForce = 500f; // Force applied to the player when exiting the circle

    private GameObject playerInstance; // Reference to the player GameObject
    public bool isInCircle = false; // Flag to indicate if the player is inside the circle
    private Vector3 exitPosition; // Position to exit the circle
    private Quaternion originalRotation; // Original rotation of the player
    public GameObject air;
    [SerializeField] float pushspeed=13f;
    [SerializeField] float pullspeed=2200f;
    bool firsttimein = false;
    [SerializeField]
    Transform center;
    void Start()
    {
        originalRotation = Quaternion.identity;
        playerInstance=GameObject.FindWithTag("Player");
    }

    void Update()
    {
        air.transform.Rotate(0, 0, 2f);
        if (isInCircle && playerInstance != null)
        {   
            if(!firsttimein)
            {
                  playerInstance.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                  firsttimein=true;
            }
            // Rotate the player around the circle
            playerInstance.transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);

            // Decrease the scale of the player
            Vector3 newScale = playerInstance.transform.localScale - Vector3.one * scaleSpeed * Time.deltaTime;
            newScale = Vector3.Max(newScale, Vector3.one * minScale);
            playerInstance.transform.localScale = newScale;
            playerInstance.GetComponent<Rigidbody2D>().AddForce(-pullspeed*Time.deltaTime*(playerInstance.transform.position-center.position).normalized,ForceMode2D.Force);

            playerInstance.transform.Rotate(0,0,15f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop the player's movement
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            // Start rotating around the circle
            isInCircle = true;
            // Assign the player's GameObject to playerInstance
            playerInstance = other.gameObject;
        }
    }
  public void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
        {

            isInCircle=false;
            playerInstance.transform.localScale = Vector3.one*0.3f;
            Debug.Log("HIT");
        }
  }
    public void ThrowPlayerOut()
    {
        // Calculate exit direction
        Vector3 exitDirection  = Random.onUnitSphere;

        // Normalize the vector to make it a unit vector
        Vector3 randomUnitVector = exitDirection.normalized;


        // Apply force to throw the player out of the circle
        Rigidbody2D playerRigidbody = playerInstance.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            firsttimein=false;
            playerRigidbody.AddForce(randomUnitVector* pushspeed,ForceMode2D.Force);
        }

        // Reset scale
        
    }
}
