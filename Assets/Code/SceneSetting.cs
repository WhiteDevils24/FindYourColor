using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetting : MonoBehaviour
{
    // Main Menu: Start the game and choose a chapter
    public void StartGame()
    {
        SceneManager.LoadScene("PickChapterScene");
    }

    // Exit the game
    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    // Load specific chapters based on selection
    public void SelectChapter(int chapterNumber)
    {
        PlayerPrefs.SetInt("SelectedChapter", chapterNumber);
        SceneManager.LoadScene("ModeSelectionScene");
    }

    // Choose Mode: Single Player or Multiplayer
    public void SinglePlayerMode()
    {
        string sceneName = GetChapterSceneName();
        if (sceneName != null)
        {
            PlayerPrefs.SetString("SelectedMode", "SinglePlayer");
            SceneManager.LoadScene(sceneName);
        }
    }

    public void MultiPlayerMode()
    {
        string sceneName = GetChapterSceneName();
        if (sceneName != null)
        {
            PlayerPrefs.SetString("SelectedMode", "MultiPlayer");
            SceneManager.LoadScene(sceneName);
        }
    }

    // Get the selected chapter scene name
    private string GetChapterSceneName()
    {
        int chapterNumber = PlayerPrefs.GetInt("SelectedChapter", -1);
        if (chapterNumber == -1)
        {
            Debug.LogError("Chapter not selected!");
            return null;
        }

        return "Chapter" + chapterNumber.ToString() + "Scene";
    }
}
