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

    void Start()
    {
        spawnTimer = new Timer(spawnInterval);
    }

    void Update()
    {
        if (spawnTimer.update() && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
        }
        
    }

    void SpawnEnemy()
    {
        // Random direction around player
        Vector3 randomDir = Random.onUnitSphere;
        Vector3 spawnPos = player.position + randomDir * spawnRadius;

        int enemyIndex = Random.Range(0, enemies.Length);

        // Face enemy toward player
        Quaternion rotation = Quaternion.LookRotation(player.position - spawnPos);

        Instantiate(enemies[enemyIndex], spawnPos, rotation);
        currentEnemies++;
    }

    void OnEnemyDeath()
    {
        currentEnemies--;
    }
}
