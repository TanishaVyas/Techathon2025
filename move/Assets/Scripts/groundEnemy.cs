using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundEnemy : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletspeed = 10f;
    public Transform player;
    public Transform gun;
    public bool canshoot = false;
    public float spreadAngle = 45f;
    public float fireRate = 1f; // Adjust this to set the rate of fire

    private float nextFireTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {   if(player == null) return;
        Vector3 directionToPlayer = (player.position - bulletSpawnPoint.transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.Euler(0f, 0f, angle);
        gun.transform.rotation = newRotation;

        if (canshoot && Time.time >= nextFireTime)
        {
            StartCoroutine(Fire());
            nextFireTime = Time.time + 1f / fireRate; // Calculate next fire time based on fire rate
        }
    }

    IEnumerator Fire()
    {
        Vector2 centerdirection = (player.position - transform.position).normalized;
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = centerdirection * bulletspeed;
        for (int i = 0; i < 5; i++)
        {
            float a = Mathf.Atan2(centerdirection.y, centerdirection.x) * Mathf.Rad2Deg + (i - 2) * spreadAngle;
            Vector2 direction = Quaternion.Euler(0, 0, a) * Vector2.right;
            bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletspeed;
        }
        yield return null;
    }
}
