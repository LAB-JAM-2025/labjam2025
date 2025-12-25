using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] GameObject trans;
    SceneChanger changer;
    [SerializeField] string toLoad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        changer = trans.GetComponent<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(toLoad == "MainMenu")
                Cursor.visible = true;
            changer.NextLevel();
        }
    }
}
