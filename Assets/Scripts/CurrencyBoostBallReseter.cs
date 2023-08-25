using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyBoostBallReseter : BallReseter
{

    [SerializeField] private int currencyBoost;
    [SerializeField] private CurrencyType currencyType;

    protected override void ResetGame()
    {
        GameStats.Instance.AddCurrency(currencyType, currencyBoost);
    }
}
