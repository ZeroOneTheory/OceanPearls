using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{

    public void Onclick_Back(){
        MenuManager.OpenMainMenu(Menu.MAIN_MENU,this.gameObject); 
    }

    public void GoToLevel(string level_name){

        SceneManager.LoadScene(level_name);

    }
}
