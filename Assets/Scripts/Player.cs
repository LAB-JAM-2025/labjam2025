using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] int hpMax;
    [SerializeField] int hp;
    [SerializeField] GameObject bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = hpMax;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp == 0)
        {
            Time.timeScale = 0;
            SoundManager.instance.StopMusic();
            /// TODO: defeat screen
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = transform.position;
            Vector3 fwd = transform.forward;
            Vector3 spawnPos = pos + fwd;

            GameObject newBullet = Instantiate(bullet, spawnPos, transform.rotation);
            newBullet.tag = "PlayerBullet";
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            Bullet b = newBullet.GetComponent<Bullet>();
            rb.linearVelocity = newBullet.transform.forward * b.getSpeed();
        }
    }

    public void changeHP(int value)
    {
        hp = Mathf.Clamp(hp + value, 0, hpMax);
    }
}
