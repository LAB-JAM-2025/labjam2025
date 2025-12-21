using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour

{
    
    [SerializeField]
    private GameObject creditsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsOn()
    {
        creditsPanel.SetActive(true);
    }

    public void CreditsOff()
    {
        creditsPanel.SetActive(false);
    }
    
    

}
