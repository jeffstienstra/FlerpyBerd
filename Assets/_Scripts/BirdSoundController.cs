using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSoundController : MonoBehaviour
{
    public AudioSource birdFlap;
    public AudioSource birdSongs;
    public AudioSource bigSquawk;
    public AudioSource birdSwoosh;
    public AudioSource getEgg;
    public AudioSource hit1;
    public AudioSource hit2;
    public AudioSource squawk;

    private void Awake()
    {
        if (!birdSongs.isPlaying)
        {
            birdSongs.Play();
        }
    }
}
