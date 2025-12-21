using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour

{
    
    [SerializeField]
    private GameObject credits;
    public GameObject main;
    public GameObject gurl;

    public void StartGame()
    {
        SceneManager.LoadScene("lashka");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsOn()
    {
        credits.SetActive(true);
        gurl.SetActive(false);
        main.SetActive(false);
    }

    public void CreditsOff()
    {
        credits.SetActive(false);
        gurl.SetActive(true);
        main.SetActive(true);
    }
    

}
