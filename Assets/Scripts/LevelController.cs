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

    //  STARTS
    void Awake()
    {
        plyCtrl = GameObject.FindObjectOfType<PlayerController>();
    }

    void Start()
    {

    }

    //  UPDATES
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            plyCtrl.ChangeAnimationState("Clarence_hit");

        }
        CheckForWin();
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
    public PlayerController GetPlayerController()
    {
        return plyCtrl;
    }
    public void LevelFailed()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
