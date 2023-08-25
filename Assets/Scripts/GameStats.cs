using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameStats : MonoBehaviour
{

    public static GameStats Instance {get; private set;}

    private int[] currencies; 
    [SerializeField] List<TextMeshProUGUI> currencyTexts;

    private void Awake()
    {
        if(Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            Instance = this; 
        } 

        currencies = new int[] {0, 0, 0, 0};
        for(int i = 0; i < currencies.Length; i++)
        {
            currencyTexts[i].text = currencies[i].ToString();
        }
    }

    public void AddCurrency(CurrencyType currencyType, int amount)
    {
        int index = (int) currencyType;
        currencies[index] += amount;
        if(currencies[index] > 99999) currencies[index] = 99999;
        currencyTexts[index].text = currencies[index].ToString();
    }
}
