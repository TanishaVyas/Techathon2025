using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAbilities : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public Transform playerTransform;

    private GameObject currentProjectile;
    private bool shooting = false;
    public GameObject fireEffect;

    // Update is called once per frame
    void Update()
    {
        
        if (playerTransform == null)
        {
            Debug.LogWarning("Player transform not assigned to ShootTowardsPlayer script.");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var effect=Instantiate(fireEffect,transform.position, Quaternion.identity);
            StartShooting();
            effect.transform.SetParent(currentProjectile.transform);
            playerTransform.GetComponent<colourChangePortal>().fireball();

        }

        if (Input.GetKeyUp(KeyCode.Space) && shooting)
        {
            StopShooting();
        }

        if (shooting && currentProjectile != null)
        {
            currentProjectile.transform.position=transform.position;

            fireEffect.transform.position=transform.position;
        }
    }

    void StartShooting()
    {
        if (currentProjectile == null)
        {
            currentProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            currentProjectile.GetComponent<fireball>().whoshot = gameObject;
            currentProjectile.GetComponent<fireball>().hasshot = false;
        }
        shooting = true;

    }

    public void StopShooting()
{       if(currentProjectile==null) return;
        currentProjectile.GetComponent<fireball>().hasshot=true;
        Rigidbody2D projectileRb = currentProjectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        projectileRb.AddForce(-direction * projectileSpeed, ForceMode2D.Impulse);
        currentProjectile=null;
        shooting = false;
    }
}
