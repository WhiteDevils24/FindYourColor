using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Method to restart the game
    public void RestartGame()
    {
        // Load the scene that was active before the game over
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Method to return to the main menu
    public void ReturnToMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("StartScreen");
    }
}
