using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallResetter : MonoBehaviour
{
    [Header("Resetting")]
    [SerializeField] private PegBallFall player;
    private bool resetting = false;
    private IEnumerator resetCoroutine = null;

    [Header("Game Effect")]
    private PurchaseItem gameEffect;
    [SerializeField] private SpriteRenderer triggerSprite;
    [SerializeField] private SpriteRenderer gameEffectSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player.gameObject && !resetting)
        {
            resetting = true;
            resetCoroutine = ResetGameCo();
            StartCoroutine(resetCoroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject == player.gameObject && resetting)
        {
            if(resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
                resetCoroutine = null;
                resetting = false;
            }
        }
    }

    private IEnumerator ResetGameCo()
    {
        yield return new WaitForSeconds(1.33f);

        ResetGame();

        StopCoroutine(ResetGameCo());
    }

    protected virtual void ResetGame()
    {
        gameEffect.ApplyEffects();
        player.ResetBall();

        resetCoroutine = null;
        resetting = false;
    }

    public void SetEffect(PurchaseItem effect)
    {
        Color effectColor = effect.effects[0].GetColor();
        gameEffect = effect;
        triggerSprite.color = new Color(effectColor.r, effectColor.g, effectColor.b, triggerSprite.color.a);
        gameEffectSprite.sprite = effect.effects[0].icon;
    }
}
