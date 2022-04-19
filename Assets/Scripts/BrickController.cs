using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public bool isBreakable = true;
    public int hits = 1;
    public string dropPower = "";

    [SerializeField]
    private SpriteRenderer sprite;

//  START
    void Awake(){

        sprite = GetComponent<SpriteRenderer>();
    }

    public void KnockOnBrick(int m_hits){
        if(isBreakable){
            hits-= m_hits;
        }
        if(hits<=0){
            BreakBrick();
        }
        
    }

    public void BreakBrick(){
        Destroy(this.gameObject);
    }

}
