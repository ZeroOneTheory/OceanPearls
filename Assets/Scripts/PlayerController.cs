using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance {get; private set;}
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
        }
        GetPlayerComponents();
    }

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
    private Vector2 currentPosition;
    private List<BallController> heldBalls = new List<BallController>();

    public BallController throwingBall {get; private set;}
    [SerializeField]
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

    void Start()
    {       
        currentPosition = transform.position;
        ballLaunchTransform = GameObject.Find("PLAYER_LAUNCH").gameObject.transform;
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
    if(throwingBall!=null){

        Vector2 launchDirection = new Vector2(launchLean, ballLaunchSpeed);
        throwingBall.LaunchBall(launchDirection);

        if (!powerStatus.Contains("catch")) 
            { releaseBall = true; }
    }
    
}
public void SetNextBallToThrow(GameObject go)
{
    myBall_GO = go;
    throwingBall = myBall_GO.GetComponent<BallController>();
}
public void FailLevel() // FailLevel Event   / UPDATE THIS
{
    LevelController.Instance.LevelFailed();
}
public void NextLevel() // Next Level Event  / UPDATE THIS!
{
    LevelController.Instance.ProgressLevel();
}
    
private void GetPlayerComponents()
{
    rb2d = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
}
private void PlayerControls()
    {
        if (!isDead)
        {
            var xAxisRaw = Input.GetAxis("Horizontal");

            currentPosition.x += Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            currentPosition.x = Mathf.Clamp(currentPosition.x, walls.x, walls.y);
            transform.position = currentPosition;

            if (xAxisRaw < 0) { launchLean = -2; } else if (xAxisRaw > 0) { launchLean = 2; }

        }


    }
private void AnimationUpdate()
    {
        var xAxisRaw = Input.GetAxis("Horizontal");

        if (!isDead && !LevelController.Instance.levelWin)
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

