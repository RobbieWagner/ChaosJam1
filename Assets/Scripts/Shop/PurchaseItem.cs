using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item")]
public class PurchaseItem : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] public int cost;
    [SerializeField] public CurrencyType currencyType;

    [SerializeReference] public List<PurchaseEffect> effects;

    [ContextMenu(nameof(AddBounceEffect))] void AddBounceEffect(){effects.Add(new AddBounce());}
    [ContextMenu(nameof(Clear))] void Clear(){effects.Clear();}

    public bool Buy()
    {
        if(GameStats.Instance.currencies[(int)currencyType] >= cost)
        {
            GameStats.Instance.AddCurrency(currencyType, -cost);
            ApplyPurchaseEffects();
            return true;
        }
        return false;
    }

    private void ApplyPurchaseEffects()
    {
        Debug.Log("bought");
        foreach(PurchaseEffect effect in effects)
        {
            effect.ApplyPurchaseEffect();
        }
    } 
}
