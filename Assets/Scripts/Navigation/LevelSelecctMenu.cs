using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelecctMenu : MonoBehaviour
{
    [SerializeField]
    private MenuManager menu_mgr;

    public void Onclick_Back(){
        menu_mgr.OpenMenu(Menu.MAIN_MENU,this.gameObject); 
    }

    public void GoToLevel(string level_name){
        
        SceneManager.LoadScene(level_name);
    }

}
