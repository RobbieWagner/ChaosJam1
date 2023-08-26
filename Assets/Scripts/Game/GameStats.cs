using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameStats : MonoBehaviour
{

    public static GameStats Instance {get; private set;}

    public int[] currencies {get; private set;} 
    [SerializeField] List<TextMeshProUGUI> currencyTexts;

    [HideInInspector] public PhysicsMaterial2D mutantMaterial;

    [HideInInspector] public int currencyAddOnPickup = 1;
    [HideInInspector] public int currencyAddOnEffect = 1;

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

        currencyAddOnPickup = 1;
        currencyAddOnEffect = 1;
    }

    public void AddCurrency(CurrencyType currencyType, int amount)
    {
        int index = (int) currencyType;
        currencies[index] += amount;
        if(currencies[index] > 99999) currencies[index] = 99999;
        currencyTexts[index].text = currencies[index].ToString();
    }

    public Color GetCurrencyColor(CurrencyType currencyType)
    {
        if(currencyType == CurrencyType.RED) return Color.red;
        if(currencyType == CurrencyType.GREEN) return Color.green;
        if(currencyType == CurrencyType.BLUE) return Color.blue;
        if(currencyType == CurrencyType.YELLOW) return Color.yellow;
        else return Color.black;
    }
}
