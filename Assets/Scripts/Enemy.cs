using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    [SerializeField] int hpMax;
    public int hp;
    GameObject sphere;
    GameObject player;
    BulletSpawner spawner;

    public static event Action OnEnemyKilled;
    public event Action OnDeath;
    EnemySpawner enemySpawner;

    [SerializeField] GameObject healthPack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = hpMax;
        sphere = GameObject.FindGameObjectWithTag("Sphere");
        player = GameObject.FindGameObjectWithTag("Player");
        spawner = GetComponent<BulletSpawner>();
        spawner.enabled = false;

        GameObject spawnSphere = GameObject.Find("SpawnSphere");
        enemySpawner = spawnSphere.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == 0)
        {
            enemySpawner.OnEnemyDeath();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {

        Vector3 fromSphere = transform.position - sphere.transform.position;
        fromSphere.Normalize();

        Vector3 toPlayer = transform.position - player.transform.position;

        Vector3 projection = -Vector3.ProjectOnPlane(toPlayer, fromSphere).normalized;

        if (projection != Vector3.zero && toPlayer.magnitude <= 10)
        {
            spawner.enabled = true;
            Quaternion targetRotation = Quaternion.LookRotation(projection, fromSphere);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        else
        {
            spawner.enabled = false;
        }
    }

    public void changeHP(int value)
    {
        hp = Mathf.Clamp(hp + value, 0, hpMax);
        if (hp == 0)
        {
            if (UnityEngine.Random.Range(0, 101) <= 25)
                Instantiate(healthPack, transform.position, transform.rotation);

            OnEnemyKilled?.Invoke();
            OnDeath?.Invoke();
            enemySpawner.OnEnemyDeath();
            Destroy(gameObject);
        }
    }
}
