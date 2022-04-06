using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Animations
    private string PLAYER_IDLE = "Clarence_idle";
    private string PLAYER_SHOOT = "Clarence_shoot";
    private string PLAYER_CHOMP = "Clarence_chomp";
    private string PLAYER_HIT = "Clarence_hit";
    private string PLAYER_LOSE = "Clarence_lose";
    private string PLAYER_MOVE_L = "Clarence_move-L";
    private string PLAYER_MOVE_R = "Clarence_move-R";
    private string PLAYER_POWERUP = "Clarence_powerup";
    private string PLAYER_SILLY = "Clarence_silly";
    #endregion

    private Vector2 move;
    private Vector2 position;
    private BallController myBallCtrl;
    [SerializeField]
    private LevelController lvlControl;
    private string currentAnimState;
    public string powerStatus = "";

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
    public float launchLean = -2;

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

        AnimationUpdate();

    }

    void FixedUpdate()
    {

        PlayerControls();

    }


    //  METHODS

public void SpitPearl()
{
    Vector2 launchDirection = new Vector2(launchLean, ballLaunchSpeed);
    myBallCtrl.LaunchBall(launchDirection);
    if (!powerStatus.Contains("catch")) { releaseBall = true; }
}
public void SetBallCtrl(GameObject go)
{
    myBall_GO = go;
    myBallCtrl = myBall_GO.GetComponent<BallController>();
}
public void FailLevel()
{
    lvlControl.LevelFailed();
}
public void NextLevel()
{
    lvlControl.ProgressLevel();
}
    
private void GetPlayerComponents()
{
    rb2d = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    lvlControl = GameObject.FindObjectOfType<LevelController>();
}
private void PlayerControls()
    {
        if (!isDead)
        {
            var xAxisRaw = Input.GetAxis("Horizontal");

            position.x += Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, walls.x, walls.y);
            transform.position = position;

            if (xAxisRaw < 0) { launchLean = -2; } else if (xAxisRaw > 0) { launchLean = 2; }

        }


    }
private void AnimationUpdate()
    {
        var xAxisRaw = Input.GetAxis("Horizontal");

        if (!isDead && !lvlControl.levelWin)
        {

            if (PlayerReleaseBall())
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
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Spit!");
                    ChangeAnimationState(PLAYER_SHOOT);
                    SpitPearl();
                }
            }


        }
    }
public void ChangeAnimationState(string newState)
    {
        if (currentAnimState == newState) return;

        anim.Play(newState);

        currentAnimState = newState;
    }

    // Properties
public bool PlayerReleaseBall()
    {
        return releaseBall;
    }

    //Collisions


}

