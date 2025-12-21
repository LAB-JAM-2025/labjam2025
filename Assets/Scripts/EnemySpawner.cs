using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject[] enemies;
    [Header("Spawn Timing")]
    public float spawnInterval; // in seconds

    [Header("Spawn Around Player")]
    [SerializeField]public Transform player;
    public float spawnRadius = 15f;

    Timer spawnTimer;
    int currentEnemies = 0;
    public int maxEnemies;
    int enemyIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxEnemies = 20;
        spawnTimer = new Timer(spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer.update() && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
        }
        KillCounter.OnSwitchWave += SwitchEnemies; 
    }

    void SpawnEnemy()
    {
        // Random direction around player
        Vector3 randomDir = UnityEngine.Random.onUnitSphere;
        Vector3 spawnPos = player.position + randomDir * spawnRadius;

        float checkColRadius = 0.5f;
        Collider[] hitColliders = Physics.OverlapSphere(spawnPos, checkColRadius);
        if (hitColliders.Length > 0)
        {
            SpawnEnemy();
            return;
        }

        //int enemyIndex = UnityEngine.Random.Range(0, enemies.Length);

        // Face enemy toward player
        Quaternion rotation = Quaternion.LookRotation(player.position - spawnPos);

        Instantiate(enemies[enemyIndex], spawnPos, rotation);
        currentEnemies++;
    }

    public void OnEnemyDeath()
    {
        Debug.Log("sosal");
        currentEnemies--;
    }
    
    void SwitchEnemies(int waveNumber)
    {
        switch (waveNumber)
        {
            case 2:
                enemyIndex = 1;
                break;
            case 3:
                enemyIndex = 2;
                break;
            default:
                Debug.Log("Unknown wave number!");
                break;
        }
    }
}
