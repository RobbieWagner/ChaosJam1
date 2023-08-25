using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [HideInInspector] public bool shopDisplayed = false;
    private List<GameObject> currentDisplayedShopItems;
    private List<PurchaseButton> shopItems;
    [SerializeField] private List<PurchaseButton> initialShopItems;

    [SerializeField] private List<VerticalLayoutGroup> shops;

    private void Awake()
    {
        shopItems = initialShopItems;
        currentDisplayedShopItems = new List<GameObject>();
        canvas.enabled = false;
    }

    public void EnterShop()
    {
        foreach(PurchaseButton shopItem in shopItems)
        {
            int index = (int) shopItem.shopItem.currencyType;
            GameObject newGameObject = Instantiate(shopItem.gameObject, shops[index].transform);
            currentDisplayedShopItems.Add(newGameObject);
        }

        canvas.enabled = true;
        shopDisplayed = true;
    }

    public void AddShopItem(PurchaseButton shopItem)
    {
        shopItems.Add(shopItem);

        if(shopDisplayed)
        {
            int index = (int) shopItem.shopItem.currencyType;
            GameObject newGameObject = Instantiate(shopItem.gameObject, shops[index].transform);
            currentDisplayedShopItems.Add(newGameObject);
        }
    }

    public void RemoveShopItem(PurchaseButton shopItem)
    {
        if(shopItem != null && shopItems.Contains(shopItem))
        {
            shopItems.Remove(shopItem);
            Destroy(shopItem.gameObject);
        }
    }

    public void LeaveShop()
    {
        foreach(GameObject item in currentDisplayedShopItems)
        {
            Destroy(item);
        }
        currentDisplayedShopItems.Clear();

        canvas.enabled = false;
        shopDisplayed = false;
    }
}
