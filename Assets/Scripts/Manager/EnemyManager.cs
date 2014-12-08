using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject[] enemies;
    public int enemiesToPool;
    public float startSpawnTime;
    private float spawnTime;
    public Transform[] spawnPoints;


    private List<GameObject> enemyPool;
    float timer;

    void Start()
    {
        timer = 0;
        spawnTime = startSpawnTime;
        enemyPool = new List<GameObject>();
        PoolEnemies();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            timer = 0;
            Spawn();
            SetSpawnTime(startSpawnTime - DifficultyManager.ReduceSpawn());
        }
    }

    void PoolEnemies()
    {
        GameObject thePool = new GameObject();
        thePool.name = "EnemyPool";
        for (int e = 0; e < enemies.Length; e++)
        {
            for (int i = 0; i < enemiesToPool / 2; i++)
            {
                GameObject enemyObj = Instantiate(enemies[e], new Vector3(0f, -10f, 0f), Quaternion.identity) as GameObject;
                enemyObj.SetActive(false);
                enemyObj.transform.parent = thePool.transform;
                enemyPool.Add(enemyObj);
            }
        }

    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Add(enemy);
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        if (enemyPool.Count > 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            int selectedEnemy = Random.Range(0, enemyPool.Count);
            GameObject theEnemy = enemyPool[selectedEnemy];

            theEnemy.GetComponent<EnemyHealth>().SetEnemyHealth(DifficultyManager.BuffHealth());
            theEnemy.transform.position = spawnPoints[spawnPointIndex].position;

            theEnemy.SetActive(true);
            theEnemy.GetComponent<NavMeshAgent>().enabled = true;
            enemyPool.RemoveAt(selectedEnemy);
        }
    }

    public void SetSpawnTime(float value)
    {
        if (value < 0.3f)
            spawnTime = 0.3f;
        else
            spawnTime = value;
    }

}
