using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector2 move;
    private Vector2 position;
    private Vector2 startPosition;
    private Vector2 lastVelocity;
    [SerializeField]
    private PlayerController plyCtrl;
    [SerializeField]
    private LevelController lvlCtrl;

    public Rigidbody2D rb2d;
    public Vector2 startDirection;
    public CircleCollider2D m_col;
    public float steepAngleOffset = .01f;
    public float bounceSpeed = 1f;
    public float maxVelocity = 20f;
    public float minVelocity = 15f;
    public bool colliderEnabled = false;

    //  START
    void Awake()
    {
        GetBallComponents();
    }

    //  UPDATES
    void Update()
    {
        lastVelocity = rb2d.velocity;
        m_col.enabled = colliderEnabled;
        if (lvlCtrl.levelWin)
        {
            rb2d.velocity = Vector2.zero;
        }

        if (rb2d.velocity.magnitude < minVelocity)
        {
            rb2d.AddRelativeForce(rb2d.velocity,ForceMode2D.Impulse);
            Debug.Log("BELOW MIN!" + rb2d.velocity.magnitude);
        }
        if (rb2d.velocity.magnitude > maxVelocity)
        {
            rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxVelocity);
            Debug.Log("ABOVE MAX!" + rb2d.velocity.magnitude);
        }
    }

    //  METHODS
    public void AddForceToBall(Vector2 force)
    {

        rb2d.AddForce(force, ForceMode2D.Impulse);
    }
    public void EnableBall()
    {
        colliderEnabled = true;
    }
    private void BallBounce(Collision2D col)
    {
        var speed = lastVelocity.magnitude;
        var direction = new Vector2(Mathf.Max(rb2d.velocity.x, maxVelocity), Mathf.Max(rb2d.velocity.y, maxVelocity));
        rb2d.velocity = lastVelocity;

    }
    private void GetBallComponents()
    {
        rb2d = GetComponent<Rigidbody2D>();
        m_col = GetComponent<CircleCollider2D>();
        lvlCtrl = GameObject.FindGameObjectWithTag("Level Control").GetComponent<LevelController>();
        plyCtrl = lvlCtrl.GetPlayerController();
        startPosition = transform.position;
    }
    private void BallOutBounds()
    {
        rb2d.velocity = lastVelocity * .5f;
        plyCtrl.isDead = true;
    }

    //  EVENTS
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(Vector2.Reflect(rb2d.velocity,col.contacts[0].normal)); 
        Debug.Log(rb2d.velocity.magnitude);

        if (col.gameObject.tag == "Bounds")
        {
            //ballBounce(col);   
            ;
        }

        if (col.gameObject.tag == "Out-Bounds")
        {
            BallOutBounds();
            plyCtrl.ChangeAnimationState("Clarence_lose");
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
            Debug.Log(Enemy);
            if (Enemy != null)
            {
                Enemy.Hit(1);
            }

        }

        if (col.gameObject.tag == "Player")
        {
            //ballBounce(col);

        }

    }

}
