using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float speed;
    [Header("Time to live")]
    [SerializeField] float TTL;
    Timer timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = new Timer(TTL);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.update())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && tag == "EnemyBullet")
        {
            GameObject player = other.gameObject;
            Player playerScript = player.GetComponent<Player>();
            playerScript.changeHP(-damage);
            Destroy(gameObject);
        }
        else if(other.tag == "Environment")
        {
            Destroy(gameObject);
        }
        else if(other.tag == "Enemy" && tag == "PlayerBullet")
        {
            GameObject enemy = other.gameObject;
            Enemy enemyScript = enemy.GetComponent<Enemy>(); 
            enemyScript.changeHP(-damage);
            Destroy(gameObject);
        }
        else if (other.tag == "EnemyBullet" && tag == "PlayerBullet")
        {
            GameObject bullet = other.gameObject;
            Destroy(bullet);
            Destroy(gameObject);
        }
    }

    public float getSpeed()
    {
        return speed;
    }
}
