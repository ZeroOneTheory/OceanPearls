using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Animations
    public string PLAYER_IDLE = "Clarence_idle";
    public string PLAYER_SHOOT = "Clarence_shoot";
    public string PLAYER_CHOMP = "Clarence_chomp";
    public string PLAYER_HIT = "Clarence_hit";
    public string PLAYER_LOSE = "Clarence_lose";
    public string PLAYER_MOVE_L = "Clarence_move-L";
    public string PLAYER_MOVE_R = "Clarence_move-R";
    public string PLAYER_POWERUP = "Clarence_powerup";
    public string PLAYER_SILLY = "Clarence_silly";
    #endregion

    private Vector2 move;
    private Vector2 position;
    private BallController myBallCtrl;
    [SerializeField]
    private LevelController lvlControl;
    private string currentAnimState;

    public Rigidbody2D rb2d;
    public Transform ballLaunchTransform;
    public GameObject myBall_GO;
    public Animator anim;
    public Vector2 walls;

    public bool releaseBall = false;
    public float ballLaunchSpeed = 10f;
    public bool isDead = false;
    public float moveSpeed = 5f;
    public float maxSpeed = 10f;
    public float launchLean=-2;

    // START  
    void Awake()
    {
        GetPlayerComponents();
    }

    void Start()
    {

        myBallCtrl = myBall_GO.GetComponent<BallController>();
        position = transform.position;
    }

    //  UPDATES
    void Update()
    {

        LaunchBallControl();

        AnimationUpdate();

    }

    void FixedUpdate()
    {

        PlayerMovement();

    }


    //  METHODS

    public void SpitPearl()
    {
        Vector2 launchDirection = new Vector2(launchLean, ballLaunchSpeed);
        myBallCtrl.AddForceToBall(launchDirection);
        releaseBall = true;
        Invoke("EnableBall", .25f);

    }
    public void FailLevel()
    {

      lvlControl.LevelFailed();

    }

    private void GetPlayerComponents()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lvlControl = GameObject.FindObjectOfType<LevelController>();

    }
    private void LaunchBallControl()
    {
        if (!releaseBall)
        {
            myBall_GO.transform.position = ballLaunchTransform.position;
        }

        if (releaseBall == false && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAnimationState(PLAYER_SHOOT);
        }
    }
    private void EnableBall()
    {
        myBallCtrl.EnableBall();
    }
    private void PlayerMovement()
    {
        if (!isDead)
        {
            var xAxisRaw = Input.GetAxis("Horizontal");
            position.x += Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, walls.x, walls.y);
            transform.position = position;
            if(xAxisRaw<0){launchLean=-2;}else if(xAxisRaw>0){launchLean=2;}
        }

    }
    private void AnimationUpdate()
    {
        var xAxisRaw = Input.GetAxis("Horizontal");

        if (!isDead && !lvlControl.levelWin)
        {

            if (releaseBall)
            {
                if (xAxisRaw != 0)
                {
                    if (xAxisRaw < 0)
                    {
                        ChangeAnimationState(PLAYER_MOVE_L);
                        
                    }
                    if (xAxisRaw > 0)
                    {
                        ChangeAnimationState(PLAYER_MOVE_R);
                        
                    }
                }
                else
                {
                    ChangeAnimationState(PLAYER_IDLE);
                }
            } else {
                ChangeAnimationState(PLAYER_IDLE);
            }

            
        }
    }
    public void ChangeAnimationState(string newState)
    {
        if (currentAnimState == newState) return;

        anim.Play(newState);

        currentAnimState = newState;
    }
}

