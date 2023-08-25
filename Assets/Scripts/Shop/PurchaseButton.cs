using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PurchaseButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] public PurchaseItem shopItem;
    [HideInInspector] public Shop shop;

    private void Awake()
    {
        button.onClick.AddListener(PurchaseItem);
    }

    private void PurchaseItem()
    {
        bool purchased = shopItem.Buy();

        if(purchased)
        {
            shop.RemoveShopItem(this);
        }
    }
}
