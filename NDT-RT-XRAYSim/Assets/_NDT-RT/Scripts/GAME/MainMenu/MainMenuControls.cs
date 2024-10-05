using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour
{
    public void OnClick_Start()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
}