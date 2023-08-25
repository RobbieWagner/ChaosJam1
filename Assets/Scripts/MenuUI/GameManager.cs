using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    private bool canEnterShop = false;
    [SerializeField] public PegBallFall ball;

    [SerializeField] private Shop upgradesShop;

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

    private void Update()
    {
        if(!ball.falling)
        {
            canEnterShop = true;
        }
        else
        {
            canEnterShop = false;
        }
    }

    private void OnToggleUpgradesShop(InputValue inputValue)
    {
        if(canEnterShop && !upgradesShop.shopDisplayed)
        {
            upgradesShop.EnterShop();
            ball.canDrop = false;
        }
        else if(upgradesShop.shopDisplayed)
        {
            upgradesShop.LeaveShop();
            ball.canDrop = true;
        }
    }
}
