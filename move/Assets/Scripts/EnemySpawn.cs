using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> enemyPrefab;


   

    public int numberOfenemies = 3;

    public float minenmeySpawnInterval = 2f;

    public float maxPlanetSpawnInterval = 5f;

    public GameObject squareArea;

    public GameObject scoreinc;

    public float score_increaser = 200;

    private float scorer;


    void Update()
    {
        SpawnEnemiesPeriodically();
        planetincreaser();
    }

    void SpawnEnemiesPeriodically()
    {
        if (Time.time % maxPlanetSpawnInterval < Time.deltaTime)
        {
            for (int i = 0; i < numberOfenemies; i++)
            {
                BoxCollider2D squareCollider = squareArea.GetComponent<BoxCollider2D>();
                Bounds squareBounds = squareCollider.bounds;

                Vector2 spawnPosition = new Vector2(Random.Range(squareBounds.min.x, squareBounds.max.x),
                                                    Random.Range(squareBounds.min.y, squareBounds.max.y));

                // Check if there's any collidable object at the spawn position
                Collider2D overlap = Physics2D.OverlapCircle(spawnPosition, 0.2f);

                if (overlap == null)
                {
                    int randomIndex = Random.Range(0, enemyPrefab.Count);
                    GameObject randomPrefab = enemyPrefab[randomIndex];
                    Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }




    void planetincreaser()
    {
        scorer = scoreinc.GetComponent<ScoreManager>().score;

        if (scorer >= score_increaser)
        {
            numberOfenemies += (int)Random.Range(2, 6);
            score_increaser += 200;
            Debug.Log("incedd");
        }

    }
}
