using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Transform ballStart;
    public GameObject ball;
    public float launchSpeed=10f;
    

    private Vector2 move;
    private Vector2 position;
    private BallController ballControl;
    [SerializeField]
    private LevelController lvlControl;
    public Animator anim;
    public const string PLAYER_IDLE = "Clarence_Idle";
    public const string PLAYER_SHOOT = "Clarence_shoot";
    public const string PLAYER_CHOMP = "Clarence_chomp";

    public bool releaseBall = false;

    public bool isDead = false;

    public Vector2 walls;
    public float moveSpeed = 5f;
    public float maxSpeed = 10f;
    // Start is called before the first frame update
    
    void Awake(){
        rb2d = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        lvlControl = GameObject.FindObjectOfType<LevelController>();
    }
    void Start()
    {
           
           ballControl = ball.GetComponent<BallController>();
           position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        BallLaunch();

    }

    void FixedUpdate(){

        PlayerMovement();
        
    }

    private void BallLaunch()
    {
        if (!releaseBall)
        {
            ball.transform.position = ballStart.position;   
        }

        if (releaseBall == false && Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play(PLAYER_SHOOT);
        }
    }

    public void SpitPearl()
    {
        Vector2 launchDirection = new Vector2(-2, launchSpeed);
        ballControl.AddForceToBall(launchDirection);
        releaseBall = true;
        Invoke("EnableBall",.25f);
    }

    public void FailLevel(){

        lvlControl.LevelFailed();
    }

    private void EnableBall()
    {
        ballControl.colliderEnabled = true;
    }

    private void PlayerMovement()
    {
        if(!isDead){
            position.x += Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, walls.x, walls.y);
            transform.position = position;
        }
        
    }
}

