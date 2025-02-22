using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float radius = 5f;
    public float force = 10f;
    public float cooldownTime = 2f; // Set the cooldown time in seconds
    private float lastShockwaveTime;

    [SerializeField]
    private Rigidbody2D rbp;

    public Transform playerTransform;
    public GameObject shock;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastShockwaveTime + cooldownTime)
        {
            lastShockwaveTime = Time.time;

            playerTransform.gameObject.GetComponent<colourChangePortal>().shock();
            Instantiate(shock, transform.position, Quaternion.identity);
            TriggerShockwave();
        }
    }

    void TriggerShockwave()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerTransform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player")) continue;

            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null && rb != rbp) // Exclude player's Rigidbody2D
            {
                Debug.Log(collider.gameObject.name);
                Vector3 direction = (collider.transform.position - playerTransform.position).normalized;
                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
