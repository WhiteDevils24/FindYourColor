using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
   public SfxManager sfxManager;
    public AudioClip buttonClickSFX;
    public float delayBeforeSceneLoad = 0.5f; // Adjust the delay to match the length of your SFX

    public void SelectChapter(int chapterNumber)
    {
        StartCoroutine(PlaySFXAndChangeScene("Chapter" + chapterNumber.ToString() + "Scene"));
    }

    public void LoadStartScreen()
    {
        StartCoroutine(PlaySFXAndChangeScene("StartScreen"));
    }

    public void RestartCurrentScene()
    {
        StartCoroutine(PlaySFXAndChangeScene(SceneManager.GetActiveScene().name));
    }

    private IEnumerator PlaySFXAndChangeScene(string sceneName)
    {
        sfxManager.PlaySFX(buttonClickSFX);
        yield return new WaitForSeconds(delayBeforeSceneLoad);
        SceneManager.LoadScene(sceneName);
    }
}
