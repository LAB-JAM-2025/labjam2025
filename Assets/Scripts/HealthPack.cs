using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] int heal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if(obj.tag == "Player")
        {
            Player player = obj.GetComponent<Player>();
            player.changeHP(heal);
            Destroy(gameObject);
        }
    }
}
