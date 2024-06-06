using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetting : MonoBehaviour
{
    //Start Scene Script
    public void ChoosePlayerScene()
    {
        SceneManager.LoadScene("ChoosePlayer");
    }

    public void ExitGame()
    {
        UnityEngine.Debug.Log("Exit Game");
        Application.Quit();
    }

    //Choose Mode Player
    public void SinglePlayerMode()
    {
        SceneManager.LoadScene("SinglePlayerScene");
    }

    public void MultiPlayerMode()
    {
        SceneManager.LoadScene("MultiPlayerScene");
    }


}
