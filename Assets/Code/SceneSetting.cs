using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetting : MonoBehaviour
{
    public void ChoosePlayerScene()
    {
        SceneManager.LoadScene("ChoosePlayer");
    }

    public void ExitGame()
    {
        UnityEngine.Debug.Log("Exit Game");
        Application.Quit();
    }

}
