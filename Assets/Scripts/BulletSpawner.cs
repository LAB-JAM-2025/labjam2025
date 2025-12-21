using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletInterval;
    public GameObject bullet;
    [Header("Attack pattern (relative to the parent)")]
    [SerializeField] Transform[] positions; // relative to the parent
    Timer attackTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackTimer = new Timer(bulletInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer.update())
        {
            Transform t = positions[Random.Range(0, positions.Length)];
            Transform parentTf = transform;
            Vector3 bulletPos = t.position;
                
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            SoundManager.PlaySoundAtPosition(SoundType.PLAYER_BULLET, parentTf.position);
            newBullet.transform.position = parentTf.position;
            newBullet.transform.rotation = parentTf.rotation;
            //newBullet.transform.Translate(bulletPos, Space.Self);
            newBullet.tag = "EnemyBullet";

            if (Random.Range(0, 101) <= 10)
            {
                float delta = Random.Range(1, 51) / 100; // seconds
                delta *= (Random.Range(0, 2) == 1) ? 1 : -1;

                attackTimer.changeInterval(attackTimer.getInterval() + delta);
            }
        }
    }
}
