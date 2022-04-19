using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Ball Kinematics")]
    public Rigidbody2D rb2d;
    public Vector2 startDirection;
    public CircleCollider2D m_col {get; private set;}

    public float steepAngleOffset = .01f;
    public float bounceSpeed = 1f;
    public float maxVelocity = 20f;
    public float minVelocity = 15f;

    private Vector2 move;
    private Vector2 position;
    private Vector2 startPosition;
    private Vector2 lastVelocity;


    [SerializeField]
    public bool isHeld {get; private set;}
    public Transform heldInPosition {get; private set;}


    //  START
    void Awake()
    {
        GetBallComponents();
    }

    //  UPDATES
    void Update()
    {
        lastVelocity = rb2d.velocity;

        if (LevelController.Instance.levelWin)
        {
            rb2d.velocity = Vector2.zero;

        } else {

            if (rb2d.velocity.magnitude < minVelocity)
            {
                rb2d.AddRelativeForce(rb2d.velocity, ForceMode2D.Impulse);

            }
            if (rb2d.velocity.magnitude > maxVelocity)
            {
                rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxVelocity);

            }

            CaughtBallMode(heldInPosition);
        }



    }

    //  METHODS
    public void AddForceToBall(Vector2 force)
    {
        rb2d.AddForce(force, ForceMode2D.Impulse);
    }
    public void LaunchBall(Vector2 force)
    {
        rb2d.AddForce(force,ForceMode2D.Impulse);
        isHeld = false;
        Invoke("EnableCollider",.25f);
        
    }
    private void EnableCollider(){
            m_col.enabled = true;
    }
    private void BallBounce(Collision2D col)
    {
        var speed = lastVelocity.magnitude;
        var direction = new Vector2(Mathf.Max(rb2d.velocity.x, maxVelocity), Mathf.Max(rb2d.velocity.y, maxVelocity));
        rb2d.velocity = lastVelocity;

    }
    public void GetBallComponents()
    {
        rb2d = GetComponent<Rigidbody2D>(); if(rb2d==null){Debug.LogError("RigidBody2D Not Found");}
        m_col = GetComponent<CircleCollider2D>(); if(m_col==null){Debug.LogError("CircleCollider Not Found");}
        startPosition = transform.position;

    }
    private void BallOutBounds()
    {
        rb2d.velocity = lastVelocity * .5f;
        Debug.Log("FROM BALL: Ball out of Bounds");
    }

    private void CaughtBallMode(Transform held_trans)
    {
        if (isHeld)
        {
            if(held_trans!=null){
                transform.position = held_trans.position;
                m_col.enabled = false;
            } else {
                isHeld = false;
            }
            
        }

    }
    //  EVENTS
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(Vector2.Reflect(rb2d.velocity,col.contacts[0].normal)); 
        //Debug.Log(rb2d.velocity.magnitude);

        if (col.gameObject.tag == "Bounds")
        {
            //ballBounce(col);   
            ;
        }

        if (col.gameObject.tag == "Out-Bounds")
        {

            Debug.Log("Ball Out Bounds!");
            
        }

        if (col.gameObject.tag == "Bricks")
        {
            //ballBounce(col);
            var BrickController = col.gameObject.GetComponent<BrickController>();
            if (BrickController != null)
            {
                BrickController.KnockOnBrick(1);
            }


        }

        if (col.gameObject.tag == "Enemy")
        {
            //ballBounce(col);
            EnemyController Enemy = col.gameObject.GetComponent<EnemyController>();
            if (Enemy != null)
            {
                Enemy.Hit(1);
            }

        }

        if (col.gameObject.tag == "Player")
        {
            if (PlayerController.Instance)
            {
                if (!PlayerController.Instance.PlayerReleaseBall())
                {
                    heldInPosition = PlayerController.Instance.ballLaunchTransform;
                    isHeld = true;
                    rb2d.velocity = Vector2.zero;
                    PlayerController.Instance.SetNextBallToThrow(this.gameObject);

                }
            }

        }

    }

}
