using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void onStartPress()
    {
        SceneManager.LoadScene("FlerpyBerd");
    }

    public void onOptionsPress()
    {
        Debug.Log("options was pressed");

    }

    public void onQuitPress()
    {
        Debug.Log("quit was pressed");
        Application.Quit();
    }

    public void onMainMenuPress()
    {
        Debug.Log("Main Menu was pressed");
        SceneManager.LoadScene("Title");
    }
}
