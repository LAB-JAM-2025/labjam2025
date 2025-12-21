using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int hpMax;
    [SerializeField] int hp;
    [SerializeField] GameObject bullet;
    [SerializeField] float attackInterval = 0.25f;
    [SerializeField] private Image hpBar;
    bool canShoot = true;
    Timer shootTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = hpMax;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        shootTimer = new Timer(attackInterval);
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

        if(!canShoot)
        {
            if(shootTimer.update())
            {
                canShoot = true;
            }
        }

        if (Input.GetMouseButton(0) && canShoot)
        {
            Vector3 pos = transform.position;
            Vector3 fwd = transform.forward;
            Vector3 spawnPos = pos + fwd;

            GameObject newBullet = Instantiate(bullet, spawnPos, transform.rotation);
            newBullet.tag = "PlayerBullet";
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            Bullet b = newBullet.GetComponent<Bullet>();
            rb.linearVelocity = newBullet.transform.forward * b.getSpeed();
            canShoot = false;
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void changeHP(int value)
    {
        Debug.Log("Player HP changed by " + hp);
        hp = Mathf.Clamp(hp + value, 0, hpMax);
        if (hpBar != null)
            hpBar.fillAmount = (float)hp / hpMax;
    }
}
