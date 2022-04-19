using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void Onclick_Start(){
        SceneManager.LoadScene("Level_0");
    }
    public void Onclick_Continue(){
        MenuManager.OpenMainMenu(Menu.LEVEL_SELECT,this.gameObject); 
    }

    public void Onclick_Settings(){

       
    }
    public void Onclick_Scores(){

       
    }
}
