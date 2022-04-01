using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float steepAngleOffset= .01f;
    public float bounceSpeed = 1f;
    public float maxVelocity = 20f;
    public float minVelocity = 15f;

    public Vector2 startDirection = new Vector2(1,-1);

    private Vector2 move;
    private Vector2 position;
    private Vector2 startPosition;
    private Vector2 lastVelocity;

    [SerializeField]
    private PlayerController plyControl;
    
    public CircleCollider2D m_col;
    [SerializeField]
    public bool colliderEnabled=false;

    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        m_col = GetComponent<CircleCollider2D>();
        
        startPosition = transform.position;
        
    }

    public void AddForceToBall(Vector2 force){

        rb2d.AddForce(force,ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb2d.velocity; 
        m_col.enabled = colliderEnabled;

    }

    void BallBounce(Collision2D col){
        var speed = lastVelocity.magnitude;
        var direction = new Vector2(Mathf.Max(rb2d.velocity.x,maxVelocity),Mathf.Max(rb2d.velocity.y,maxVelocity));
        rb2d.velocity = lastVelocity;

    }


    void OnCollisionEnter2D(Collision2D col){
            if(col.gameObject.tag == "Bounds"){
                //ballBounce(col);     
            }

            if(col.gameObject.tag == "Out-Bounds"){
                rb2d.velocity = lastVelocity*.5f;
                plyControl.isDead = true;
                plyControl.anim.Play("Clarence_lose");
            }

            if(col.gameObject.tag == "Bricks"){       
                //ballBounce(col);
                col.gameObject.SetActive(false);
            }

            if(col.gameObject.tag == "Player"){
                //ballBounce(col);
                Debug.Log(rb2d.velocity);
            }
            
        }
    

    
}
