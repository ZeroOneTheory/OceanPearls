using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private MenuManager menu_mgr;

    public void Onclick_Start(){
        SceneManager.LoadScene("Level_0");
    }
    public void Onclick_Continue(){
        menu_mgr.OpenMenu(Menu.LEVEL_SELECT,this.gameObject); 
    }

    public void Onclick_Settings(){

       
    }
    public void Onclick_Scores(){

       
    }
}
