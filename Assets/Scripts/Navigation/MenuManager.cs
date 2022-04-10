using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class MenuManager: MonoBehaviour
{
    public  bool isInititialized {get; private set;}
    public  GameObject menu_main, menu_settings, menu_levelSelect, menu_scores;

    public void OpenMenu(Menu menu, GameObject callingmenu){

        
        switch(menu){

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
