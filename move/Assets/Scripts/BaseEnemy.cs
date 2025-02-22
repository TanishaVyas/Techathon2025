using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public GameObject player;
    public float radius = 5f;
    protected Renderer rendererComponent;
    public float timerDuration = 1f;
    protected float timer = 0f;
    protected bool timerRunning = false;

    protected virtual void Start()
    {
        rendererComponent = GetComponent<Renderer>();
        rendererComponent.material.color = Color.white;
        player = GameObject.FindWithTag("Player");
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rendererComponent.material.color = Color.red;

            if (!timerRunning)
            {
                timer = timerDuration;
                timerRunning = true;
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rendererComponent.material.color = Color.white;
            timerRunning = false;
            timer = 0f;
        }
    }

    protected virtual void Update()
    {
        if (timerRunning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                TimerEnded();
                timerRunning = false;
                Destroy(gameObject);
            }
        }
    }

    protected virtual void TimerEnded()
    {
        Debug.Log("Timer has ended!");
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
