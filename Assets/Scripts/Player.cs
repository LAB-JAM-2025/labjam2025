using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int hpMax;
    public int hp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp == 0)
        {
            Time.timeScale = 0;
            /// TODO: defeat screen
        }
    }

    public void changeHP(int value)
    {
        if(hp + value < 0)
            hp = 0;
        else if (hp + value > hpMax) 
            hp = hpMax;
        else
            hp += value;
    }
}
