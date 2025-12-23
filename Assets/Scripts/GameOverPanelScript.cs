using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelScript : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("lashka");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
