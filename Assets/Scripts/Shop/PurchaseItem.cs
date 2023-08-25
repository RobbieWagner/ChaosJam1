using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item")]
public class PurchaseItem : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] public int cost;
    [SerializeField] public CurrencyType currencyType;

    [SerializeReference] public List<GameEffect> effects;

    [ContextMenu(nameof(AddBounceEffect))] void AddBounceEffect(){effects.Add(new AddBounce());}
    [ContextMenu(nameof(AddCurrencyMultiplierEffect))] void AddCurrencyMultiplierEffect(){effects.Add(new AddCurrencyMultiplier());}
    [ContextMenu(nameof(AddRowEffect))] void AddRowEffect(){effects.Add(new AddRow());}

    [ContextMenu(nameof(AddCurrencyEffect))] void AddCurrencyEffect(){effects.Add(new AddCurrency());}
    [ContextMenu(nameof(Clear))] void Clear(){effects.Clear();}

    public bool Buy()
    {
        if(GameStats.Instance.currencies[(int)currencyType] >= cost)
        {
            GameStats.Instance.AddCurrency(currencyType, -cost);
            ApplyEffects();
            return true;
        }
        return false;
    }

    public void ApplyEffects()
    {
        foreach(GameEffect effect in effects)
        {
            effect.ApplyPurchaseEffect();
        }
    } 
}
