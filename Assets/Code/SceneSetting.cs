using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetting : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PickChapterScene");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void SelectChapter(int chapterNumber)
    {
        PlayerPrefs.SetInt("SelectedChapter", chapterNumber);
        SceneManager.LoadScene("ModeSelectionScene");
    }

    public void SinglePlayerMode()
    {
        PlayerPrefs.SetString("SelectedMode", "SinglePlayer");
        LoadSelectedChapter();
    }

    public void MultiPlayerMode()
    {
        PlayerPrefs.SetString("SelectedMode", "MultiPlayer");
        LoadSelectedChapter();
    }

    private void LoadSelectedChapter()
    {
        string sceneName = GetChapterSceneName();
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private string GetChapterSceneName()
    {
        int chapterNumber = PlayerPrefs.GetInt("SelectedChapter", -1);
        if (chapterNumber == -1)
        {
            Debug.LogError("Chapter not selected!");
            return null;
        }

        string sceneName = "Chapter" + chapterNumber.ToString() + "Scene";
        if (!SceneExists(sceneName))
        {
            Debug.LogError($"Scene '{sceneName}' couldn't be loaded because it has not been added to the build settings or the AssetBundle has not been loaded.");
            return null;
        }

        return sceneName;
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string scene = System.IO.Path.GetFileNameWithoutExtension(path);
            if (scene == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
