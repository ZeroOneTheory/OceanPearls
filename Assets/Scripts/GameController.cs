using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameController: MonoBehaviour
{
    public static GameController Instance {get; private set;}
   
    private void Awake(){
        Instance = this;
        
    }
    
    
}
