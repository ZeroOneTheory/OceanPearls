using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public static class MenuManager
{
    public static  bool isInititialized {get; private set;}
    public static GameObject menu_main, menu_settings, menu_levelSelect, menu_scores;

    public static void InitMainMenu(){
        menu_main = GameObject.Find("MAIN_MENU");
        //menu_settings = Msettings;
        menu_levelSelect = GameObject.Find("LEVEL_SELECT");
        //menu_scores = Mscores;
        isInititialized = true;
    }

    public static void OpenMainMenu(Menu menu, GameObject callingmenu){
        if(!isInititialized){
            InitMainMenu();
            
            if(menu_main==null){ Debug.Log("No MAIN_MENU Found"); return;}
            if(menu_levelSelect==null){ Debug.Log("No LEVEL_SELECT Found"); return;}
        }
   
        switch(menu)
        {
            case Menu.MAIN_MENU: 
            menu_main.SetActive(true);
            break;

            case Menu.SETTINGS: 
            menu_settings.SetActive(true);
            break;

            case Menu.LEVEL_SELECT: 
            menu_levelSelect.SetActive(true);
            break;

            case Menu.SCORES: 
            menu_scores.SetActive(true);
            break;
            
        }
        callingmenu.SetActive(false);
    }
}
