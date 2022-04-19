using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelController : MonoBehaviour
{
   
    public static LevelController Instance {get; private set;}
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
        }

        FetchPlayerInstance();

    }


    public PlayerController plyCtrl {get; private set;}
    public GameObject _Player {get; private set;}

    public bool levelWin {get; private set;}
    public bool levelInProgress {get; private set;}
    public int replays = 3; // Link Back to GameController

    [SerializeField]
    public GameObject ballPrefab;
    [SerializeField]
    public Transform  ballSpawnPoint;

    private int lastBrickCount = 0;
    private bool willRestart = false;

    void Start()
    {
        EventManager.ExampleEvent += CreateBall;
        willRestart = false;
    }

    //  UPDATES
    void Update()
    {
        LevelControlKeys();
        if(levelInProgress){
            CheckForWin();
        }
    }

    private void LevelControlKeys()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            willRestart = true;
            LevelFailed();

        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            CreateBall();
        }
    }

    private void CreateBall()
    {
        if(ballPrefab!=null){
            var newBall = GameObject.Instantiate(ballPrefab);
            var newBallCtrl = newBall.GetComponent<BallController>();


            if(ballSpawnPoint==null){
                if(FetchPlayerInstance()){
                    ballSpawnPoint = PlayerController.Instance.ballLaunchTransform;  
                } else {
                    ballSpawnPoint.position = gameObject.transform.position; // CHANGE THIS!
                }
            }
            newBall.transform.position = ballSpawnPoint.position;
            newBall.gameObject.tag = "Pearls";
            Vector2 launchDirection = new Vector2(-2, 12);
            
            newBallCtrl.LaunchBall(launchDirection);
            newBallCtrl.GetBallComponents();
        }
        
    }

    private bool FetchPlayerInstance()
    {

        if (PlayerController.Instance != null)
        {
            plyCtrl = PlayerController.Instance;
            _Player = PlayerController.Instance.gameObject;
            return true;
        }
        else { Debug.Log("LevelController: _Player Not Found/Assigned"); }
        return false;
    }

    //  METHODS
    
    public void CheckForWin()
    {
        if (levelWin == false)
        {
            lastBrickCount = BrickCount(true);

            if (BrickCount(false) <= 0)
            {
                EndLevel();
            }
        }

    }
    public int BrickCount(bool includeUnbreakables){
        int unbreakables = 0;
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Bricks");
            foreach (GameObject b in bricks)
            {
                BrickController b_bCtrl = b.GetComponent<BrickController>();
                if (b_bCtrl.isBreakable == false)
                {
                    unbreakables++;
                }
            }
        if(includeUnbreakables){return bricks.Length;}
        return bricks.Length - unbreakables;
    }
    public int BallCounts()
    {
        int ball_count = 0;
            GameObject[] balls;
            balls = GameObject.FindGameObjectsWithTag("Pearls");
            ball_count = balls.Length;
            return ball_count;

    }
    public void LevelFailed()
    {
        if(replays>0){
            willRestart=true;
            replays-=1;
        }

        if (willRestart)
        {
            Scene scene = SceneManager.GetActiveScene();
            FetchPlayerInstance();
            StartLevel();
            willRestart = false;
            SceneManager.LoadScene(scene.name);

        }
        else
        {
            SceneManager.LoadScene("Main_Menu");
            
        }


    }
    public void ProgressLevel()
    {
        var level_name = "Main_Menu";
        StartLevel();
        SceneManager.LoadScene(level_name);
        
    }
    public void StartLevel(){
        levelWin = false;
        levelInProgress = true;

        CreateBall();
        Debug.Log("Level Start");
    }
    public void EndLevel(){

        if(FetchPlayerInstance()){
            plyCtrl.ChangeAnimationState("Clarence_silly");
        }
        
        levelWin = true;
        levelInProgress = false;
        Debug.Log("End Level");
    }
}
