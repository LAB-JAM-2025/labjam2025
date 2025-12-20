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
            foreach (Transform t in positions)
            {
                Transform parentTf = transform;
                Vector3 bulletPos = t.position;
                
                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.transform.position = parentTf.position;
                newBullet.transform.rotation = parentTf.rotation;
                newBullet.transform.Translate(bulletPos, Space.Self);

                Rigidbody rb = newBullet.GetComponent<Rigidbody>();
                rb.linearVelocity = transform.forward.normalized * bulletSpeed;
            }
        }
    }
}
