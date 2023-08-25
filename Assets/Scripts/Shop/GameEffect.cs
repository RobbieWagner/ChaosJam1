using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEffect
{
    [SerializeField] private int magnitude;
    public virtual void ApplyPurchaseEffect()
    {
        Debug.Log("applied new effect");
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
