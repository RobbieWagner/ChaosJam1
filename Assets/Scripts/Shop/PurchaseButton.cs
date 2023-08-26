using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class PurchaseButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] public List<PurchaseItem> shopItems;
    [SerializeField] private int currentItemIndex = 0;
    [HideInInspector] public Shop shop;

    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image icon;

    [HideInInspector] public bool canInteract = false;

    private void Awake()
    {
        button.onClick.AddListener(PurchaseItem);

        itemText.text = shopItems[0]._name;
        costText.text = shopItems[0].cost.ToString();

        if(GameStats.Instance.currencies[(int) shopItems[0].currencyType] >= shopItems[0].cost)
        {
            costText.color = GameStats.Instance.GetCurrencyColor(shopItems[0].currencyType);
        }
        else costText.color = Color.black;

        icon.sprite = shopItems[0].effects[0].icon;
    }

    private void PurchaseItem()
    {
        if(canInteract)
        {
            bool purchased = shopItems[currentItemIndex].Buy();

            if(purchased)
            {
                if(currentItemIndex < shopItems.Count - 1) 
                {
                    currentItemIndex++;
                    itemText.text = shopItems[currentItemIndex]._name;
                    costText.text = shopItems[currentItemIndex].cost.ToString();

                    if(GameStats.Instance.currencies[(int) shopItems[currentItemIndex].currencyType] >= shopItems[currentItemIndex].cost)
                    {
                        costText.color = GameStats.Instance.GetCurrencyColor(shopItems[currentItemIndex].currencyType);
                    }
                    else costText.color = Color.black;
                }
                else 
                {
                    canInteract = false;
                    costText.text = "MAX";
                    costText.color = Color.black;
                }
            }
        }
    }
}
