using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hpMax;
    public int hp;
    public static event Action OnEnemyKilled;
    public event Action OnDeath;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeHP(int value)
    {
        hp = Mathf.Clamp(hp + value, 0, hpMax);
        if (hp == 0)
        {
            OnEnemyKilled?.Invoke();
            OnDeath?.Invoke();
            Destroy(gameObject);
        }

    }
}
