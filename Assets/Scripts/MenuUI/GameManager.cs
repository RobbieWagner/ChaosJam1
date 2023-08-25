using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}
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

    public IEnumerator SaveGame()
    {
        yield return null;
        OnSaveGame();
        StopCoroutine(SaveGame());
    }

    public delegate void SaveGameDelegate();
    public event SaveGameDelegate OnSaveGame;
}
