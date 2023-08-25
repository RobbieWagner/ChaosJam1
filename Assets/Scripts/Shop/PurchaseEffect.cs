using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PurchaseEffect
{
    [SerializeField] private int magnitude;
    public virtual void ApplyPurchaseEffect()
    {
        Debug.Log("applied new effect");
    }
}

[Serializable]
public class AddBounce: PurchaseEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();

        Debug.Log("added bounce");
    }
}

[Serializable]
public class AddCurrencyMultiplier: PurchaseEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();

        Debug.Log("added currency multiplier");
    }
}
