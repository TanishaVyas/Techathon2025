using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainaudio : MonoBehaviour
{
   public AudioClip backgroundClip;
    private AudioSource audioSource;
    private static mainaudio instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
        }

        audioSource.clip = backgroundClip;
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (audioSource != null && backgroundClip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Background music clip or AudioSource not set!");
        }
    }

    public void StopBackgroundMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        else
        {
            Debug.LogError("AudioSource not found!");
        }
    }
}