using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSoundControllerTitle : MonoBehaviour
{
    public static BirdSoundControllerTitle Instance;

    public AudioSource birdSongs;
    public AudioSource bigSquawk;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!Instance.birdSongs.isPlaying)
        {
            birdSongs.Play();
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
