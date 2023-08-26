using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    None = -1,
    RED = 0,
    BLUE = 1,
    GREEN = 2,
    YELLOW = 3,
}

public class CurrencyPickup : MonoBehaviour
{
    [HideInInspector] public CurrencyType currencyType;
    [SerializeField] private Color[] currencyColors;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector3 rotationVector;
    [SerializeField]private float rotationXMin;
    [SerializeField]private float rotationXMax;
    [SerializeField]private float rotationZMin;
    [SerializeField]private float rotationZMax;

    [SerializeField]private float cooldownTime = 1f;

    private bool touched = false;

    private void Awake()
    {
        currencyType = (CurrencyType) ((int) Random.Range(0, 4));

        spriteRenderer.color = currencyColors[(int)currencyType];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!touched)
            {
                StartCoroutine(TouchCurrencyPickup());
            }
        }
    }

    private IEnumerator TouchCurrencyPickup()
    {
        touched = true;

        //Debug.Log(GameStats.Instance.currencyAddOnPickup.ToString());
        GameStats.Instance.AddCurrency(currencyType, GameStats.Instance.currencyAddOnPickup);
        yield return spriteRenderer.DOColor(new Color(0,0,0,0), cooldownTime).SetEase(Ease.Linear).WaitForCompletion();
        currencyType = (CurrencyType) ((int) Random.Range(0, 4));
        yield return new WaitForSeconds(.4f);
        yield return spriteRenderer.DOColor(currencyColors[(int)currencyType], cooldownTime).SetEase(Ease.Linear).WaitForCompletion();

        touched = false;
        StopCoroutine(TouchCurrencyPickup());
    }
}
