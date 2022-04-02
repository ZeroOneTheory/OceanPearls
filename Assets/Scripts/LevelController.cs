using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private PlayerController plyCtrl;

//  STARTS
    void Awake(){      
        plyCtrl = GameObject.FindObjectOfType<PlayerController>();
    }

//  UPDATES
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            plyCtrl.ChangeAnimationState("Clarence_hit");

        }
    }

//  METHODS
    public PlayerController GetPlayerController(){
        return plyCtrl;
    }
    public void LevelFailed()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
