using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("PlaySFX: AudioClip is null");
            return;
        }

        Debug.Log("Playing SFX: " + clip.name);
        audioSource.PlayOneShot(clip);
    }
}
