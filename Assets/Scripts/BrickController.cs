using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public bool isBreakable = true;
    public int hits = 1;
    private LevelController lvlControl;

    [SerializeField]
    private SpriteRenderer sprite;

//  START
    void Awake(){

        sprite = GetComponent<SpriteRenderer>();
        lvlControl = GameObject.FindObjectOfType<LevelController>();
    }

}
