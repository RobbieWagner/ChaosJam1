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
    [SerializeField] public PurchaseItem shopItem;
    [HideInInspector] public Shop shop;

    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private TextMeshProUGUI costText;

    [HideInInspector] public bool canInteract = false;

    private void Awake()
    {
        button.onClick.AddListener(PurchaseItem);

        itemText.text = shopItem.name;
        costText.text = shopItem.cost.ToString();

        if(GameStats.Instance.currencies[(int) shopItem.currencyType] >= shopItem.cost)
        {
            costText.color = GameStats.Instance.GetCurrencyColor(shopItem.currencyType);
        }
        else costText.color = Color.black;
    }

    private void PurchaseItem()
    {
        if(canInteract)
        {
            bool purchased = shopItem.Buy();

            if(purchased)
            {
                shop.RemoveShopItem(this);
            }
        }
    }
}
