using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject[] enemies;
    public float spawnInterval; // in seconds
    public float distanceFromSphere;
    Vector3 pos;
    Vector3 radius;
    Timer spawnTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 scaleRadius = transform.localScale / 2;
        float dist = distanceFromSphere;
        radius = new Vector3(scaleRadius.x + dist, scaleRadius.y + dist, scaleRadius.z + dist);
        pos = transform.position;
        spawnTimer = new Timer(spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer.update())
        {
            Vector3 enemyPos = pos + radius;
            float angleX = Random.Range(0, 360);
            float angleY = Random.Range(0, 360);
            float angleZ = Random.Range(0, 360);
            enemyPos.Set(enemyPos.x * Mathf.Sin(angleX), enemyPos.y * Mathf.Sin(angleY), enemyPos.z * Mathf.Sin(angleZ));

            Quaternion rotation = Quaternion.Euler(angleX, angleY, angleZ);

            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(enemy, enemyPos, rotation);
        }
    }
}
