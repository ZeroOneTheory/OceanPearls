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
        if (col.gameObject.tag == "Bounds")
        {
            //ballBounce(col);     
        }

        if (col.gameObject.tag == "Out-Bounds")
        {
            BallOutBounds();
            plyCtrl.ChangeAnimationState("Clarence_lose");
        }

        if (col.gameObject.tag == "Bricks")
        {
            //ballBounce(col);
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "Player")
        {
            //ballBounce(col);
            Debug.Log(rb2d.velocity);
        }

    }

}
