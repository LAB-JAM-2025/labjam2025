using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public SceneChanger instance;
    [SerializeField] Animator transitionAnimScene;
    [SerializeField] Animator transitionAnimHUD;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {

    }

    void Start()
    {
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    IEnumerator LoadLevel()
    {
        transitionAnimScene.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnimScene.SetTrigger("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
