using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private PlayerController plyCtrl;
    [SerializeField]
    private int lastBrickCount = 0;
    public bool levelWin = false;
    public int replays = 3;
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    public List<string> levels = new List<string>();

    public GameObject prefab;
    public Wave[] waves;
    

    Wave currentWave;
    int currentWaveNumber;
    int prefabsRemainingToSpawn;
    float nextSpawnTime;

    [System.Serializable]
    public class Wave{
        public int prefabCount;
        public float timeBetweenSpawns;
    }

    //  STARTS
    void Awake()
    {
        plyCtrl = GameObject.FindObjectOfType<PlayerController>();
        EventManager.ExampleEvent += CreateBall;
    }

    void Start(){
        NextWave();
    }

    //  UPDATES
    void Update()
    {
        LevelControlKeys();

        CheckForWin();

        if( prefabsRemainingToSpawn >0 && Time.time > nextSpawnTime){
            prefabsRemainingToSpawn --;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            //Instantiate the object:  Controller control = Instantiate(prefab, Vector2.zero, Quaternion.identity) as Controller;
        }
    }

    void NextWave(){
        currentWaveNumber ++;
        currentWave = waves[currentWaveNumber-1];

        prefabsRemainingToSpawn = currentWave.prefabCount;
    }

    private void LevelControlKeys()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            plyCtrl.ChangeAnimationState("Clarence_hit");

        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            CreateBall();

        }
    }

    private void CreateBall()
    {
        var newBall = GameObject.Instantiate(ballPrefab);
        newBall.transform.position = ballSpawnPoint.position;
        newBall.gameObject.tag = "Pearls";
        Vector2 launchDirection = new Vector2(-2, 12);
        var newBallCtrl = newBall.GetComponent<BallController>();
        newBallCtrl.LaunchBall(launchDirection);
        newBallCtrl.GetBallComponents();
    }

    //  METHODS

    public void CheckForWin()
    {


        if (levelWin == false)
        {
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
            lastBrickCount = bricks.Length;

            if (bricks.Length - unbreakables <= 0)
            {
                Debug.Log("WIN!");
                plyCtrl.ChangeAnimationState("Clarence_silly");
                levelWin = true;
            }
        }



    }
    public int BallCounts()
    {
        int ball_count=0;
        if (levelWin == false)
        {
            
            GameObject[] balls;
            balls = GameObject.FindGameObjectsWithTag("Pearls");
            ball_count = balls.Length;
            return ball_count;  

        }

        return 0;

    }
    public PlayerController GetPlayerController()
    {
        return plyCtrl;
    }
    public void LevelFailed()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void ProgressLevel()
    {
        var level_name = "StageScene";
        switch (SceneManager.GetActiveScene().name)
        {

            case "StageScene": level_name = "Level_0"; break;
            case "Level_0": level_name = "Level_1"; break;
            case "Level_1": level_name = "Level_0"; break;

        }

        SceneManager.LoadScene(level_name);
    }
}
