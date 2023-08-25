using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEffect
{
    [SerializeField] protected int magnitude;
    [SerializeField] protected Color associatedColor = Color.black;
    [SerializeField] public Sprite icon;

    public virtual void ApplyPurchaseEffect()
    {
        
    }

    public Color GetColor()
    {
        return associatedColor;
    }
}

[Serializable]
public class AddBounce: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();

        Debug.Log("added bounce");
    }
}

[Serializable]
public class AddCurrencyMultiplier: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();

        Debug.Log("added currency multiplier");
    }
}

[Serializable]
public class AddCurrency: GameEffect
{
    [SerializeField] public CurrencyType currencyType;

    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        GameStats.Instance.AddCurrency(currencyType, magnitude);
    }
}

[Serializable]
public class AddRow: GameEffect
{
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private float height;

    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        DropperGame.Instance.AddRow(rowPrefab, height);
    }
}
