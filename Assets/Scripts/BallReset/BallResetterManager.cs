using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallResetterManager : MonoBehaviour
{
    [SerializeField] private List<PurchaseItem> resetEffects;
    [SerializeField] private List<BallResetter> resetters;

    private void Awake()
    {
        GameManager.Instance.ball.OnResetBall += UpdateBallResetters;
    }

    private void UpdateBallResetters()
    {
        for(int i = 0; i < resetters.Count; i++)
        {
            resetters[i].SetEffect(resetEffects[Random.Range(0, resetEffects.Count)]);
        }
    }
}
