using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSounds : MonoBehaviour
{
    [SerializeField] private AudioSource dialogueSound;
    [SerializeField] private AudioSource bumpSound;
    [SerializeField] private AudioSource pickupSound;
    [SerializeField] private AudioSource music;

    public static GameSounds Instance {get; private set;}

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void PlayDialogueSound()
    {
        if(!dialogueSound.isPlaying)dialogueSound.Play();
    }

    public void PlayBumpSound()
    {
        bumpSound.Play();
    }

    public void PlayPickupSound()
    {
        pickupSound.Play();
    }

    public void PlayMusic()
    {
        music.Play();
    }
}
