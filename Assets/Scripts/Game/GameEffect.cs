using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEffect
{
    [SerializeField] protected float magnitude;
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

        GameStats.Instance.mutantMaterial.bounciness = magnitude;
    }
}

[Serializable]
public class AddCurrencyMultiplier: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();

        GameStats.Instance.currencyAddOnPickup = (int) magnitude;
    }
}

[Serializable]
public class AddCurrency: GameEffect
{
    [SerializeField] public CurrencyType currencyType;

    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        GameStats.Instance.AddCurrency(currencyType, GameStats.Instance.currencyAddOnEffect);
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

[Serializable]
public class UpgradeFloatTime: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        GameManager.Instance.ball.maxFloatTime = magnitude;
    }
}

[Serializable]
public class UpgradeFloatSpeed: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        GameManager.Instance.ball.floatSpeed = magnitude;
    }
}

[Serializable]
public class UpgradeBasketReward: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        GameStats.Instance.currencyAddOnEffect = (int) magnitude;
    }
}

[Serializable]
public class AddMass: GameEffect
{
    public override void ApplyPurchaseEffect()
    {
        base.ApplyPurchaseEffect();
        GameManager.Instance.ball.rb2d.mass = magnitude;
    }
}
