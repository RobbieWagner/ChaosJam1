using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PegBallFall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    private bool waitingToMove = false;
    private IEnumerator movementCoroutine = null;

    private bool falling = false;
    //private PlayerInputActions playerControls;

    [Header("Ball Swing")]
    [SerializeField] private float maxXSwingPosition;
    [SerializeField] private float minXSwingPosition;
    [SerializeField] private Vector3 startingPosition = Vector3.zero;

    private void Awake()
    {
        rb2d.gravityScale = 0;

        //playerControls = new PlayerInputActions();
        StartCoroutine(BallSwing());
    }

    private void Update()
    {
        //while falling, make sure the ball is always moving
        if(falling)
        {
            if(rb2d.velocity == Vector2.zero)
            {
                waitingToMove = true;
                if(movementCoroutine == null)
                {
                    movementCoroutine = WaitToMovePlayer();
                    StartCoroutine(WaitToMovePlayer());
                }
            }
            else
            {
                waitingToMove = false;
            }
        }
        //if not falling, keep the ball from falling.
        else
        {
            rb2d.gravityScale = 0;
        }
    }

    private IEnumerator WaitToMovePlayer()
    {
        float timer = 0f;
        float timeToWait = .33f;

        while(timer <= timeToWait && waitingToMove)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        if(timer > timeToWait)
        {
            if(transform.position.x <= 0) rb2d.velocity = Vector2.right/3;
            else rb2d.velocity = Vector2.left/3;
        }

        movementCoroutine = null;
        StopCoroutine(WaitToMovePlayer());
    }

    private IEnumerator BallSwing()
    {
        bool swingRight = true;
        Vector2 maxPos = new Vector2(transform.position.x + maxXSwingPosition, transform.position.y);
        Vector2 minPos = new Vector2(transform.position.x + minXSwingPosition, transform.position.y);
        float speed = 4f;
        
        while(!falling)
        {
            if(swingRight)
            {
                transform.position = Vector2.MoveTowards(transform.position, maxPos, speed * Time.deltaTime);
                yield return null;
                if(Vector2.Distance(transform.position, maxPos) < .01f)
                {
                    swingRight = false;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, minPos, speed * Time.deltaTime);
                yield return null;
                if(Vector2.Distance(transform.position, minPos) < .01f)
                {
                    swingRight = true;
                }
            }
        }

        StopCoroutine(BallSwing());
    }

    private void OnDropButton()
    {
        if(!falling)
        {
            rb2d.gravityScale = 1;
            falling = true;
        }
    }

    public void ResetBall()
    {
        transform.position = startingPosition;
        falling = false;
    }
}