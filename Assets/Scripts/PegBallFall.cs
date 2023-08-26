using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PegBallFall : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb2d;
    private bool waitingToMove = false;
    private IEnumerator movementCoroutine = null;

    [HideInInspector] public bool falling {get; private set;}
    [HideInInspector] public bool canDrop = true;

    [Header("Ball Swing")]
    [SerializeField] private float maxXSwingPosition;
    [SerializeField] private float minXSwingPosition;
    [SerializeField] private Vector3 startingPosition = Vector3.zero;

    //Extras
    bool floating = false;
    [HideInInspector] public float springForce = 0;
    [HideInInspector] public float floatSpeed;
    [HideInInspector] public float maxFloatTime = .5f;
    private float floatTime;
    [SerializeField] private SpriteRenderer floatSprite;

    private void Awake()
    {
        GameStats.Instance.mutantMaterial = rb2d.sharedMaterial;
        rb2d.sharedMaterial.bounciness = 0;
        floatSpeed = 2;
        ResetBall();
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

        if(floating)
        {
            floatTime -= Time.deltaTime;
            if(floatTime <= 0)
            {
                floating = false;
                rb2d.gravityScale = 1;
                floatSprite.enabled = false;
                rb2d.velocity = Vector2.zero;
            }
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
        float speed = 6f;
        
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
        if(falling && floatTime > 0)
        {
            if(!floating)
            {
                floating = true;
                rb2d.gravityScale = 0;
                floatSprite.enabled = true;
                rb2d.velocity = Vector2.zero;
                rb2d.angularVelocity = 0;
            }
            else if(floating)
            {
                floating = false;
                rb2d.gravityScale = 1;
                floatSprite.enabled = false;
                rb2d.velocity = Vector2.zero;
            }
        }

        if(!falling && canDrop)
        {
            rb2d.gravityScale = 1;
            falling = true;
        }
    }

    private void OnMove(InputValue value)
    {
        if(floating)
        {
            float movement = value.Get<float>();

            if(movement >= 0f) rb2d.velocity = new Vector2(floatSpeed, 0);
            else rb2d.velocity = new Vector2(-floatSpeed, 0);
        }
    }

    public void ResetBall()
    {
        transform.position = startingPosition;
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0;
        floatSprite.enabled = false;
        falling = false;
        canDrop = true;
        rb2d.gravityScale = 0;
        floatTime = maxFloatTime;
        StartCoroutine(BallSwing());
        OnResetBall?.Invoke();
    }

    public delegate void OnResetBallDelegate();
    public event OnResetBallDelegate OnResetBall;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            GameSounds.Instance.PlayBumpSound();
        }
    }
}
