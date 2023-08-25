using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReseter : MonoBehaviour
{
    [SerializeField] private PegBallFall player;
    private bool resetting = false;
    private IEnumerator resetCoroutine = null;

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
        yield return new WaitForSeconds(2f);

        ResetGame();

        StopCoroutine(ResetGameCo());
    }

    protected virtual void ResetGame()
    {
        player.ResetBall();

        resetCoroutine = null;
        resetting = false;
    }
}
