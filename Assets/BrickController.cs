using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public bool isBreakable = true;
    public int hits = 1;

    [SerializeField]
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Awake(){
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
