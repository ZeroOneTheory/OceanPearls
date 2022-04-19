using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb2d;
    public SpriteRenderer sprite;
    public Animator anim;
    public int hitpoints = 1;
    public float moveSpeed;
    public float changeDirectionTime = 15;
    public float maxVelocity;
    public float changeDir = 15;


    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        EnemyAnimation();
    }

    private Vector2 EnemyMove()
    {
        Vector2 faceDirection = rb2d.velocity;
        if (changeDir < changeDirectionTime/2)
        {
            faceDirection = Vector2.up * new Vector2( Random.Range(15,-15),moveSpeed) ;
            rb2d.AddRelativeForce(faceDirection * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
            if (changeDir <= 0) { changeDir = changeDirectionTime; }
        }
        else
        {
            if (target != null)
            {
                faceDirection = target.position;
                faceDirection.x = target.position.x - transform.position.x;
                faceDirection.y = target.position.y - transform.position.y;

                rb2d.AddRelativeForce(faceDirection.normalized * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }

        }
        changeDir -= 1f * Time.deltaTime;
        if(rb2d.velocity.magnitude>maxVelocity){
            rb2d.velocity= Vector2.ClampMagnitude(rb2d.velocity,maxVelocity);
        }
        return faceDirection;
    }

    private void EnemyAnimation(){
        if(rb2d.velocity.x<0){
            sprite.flipX = false;
        } else {
            sprite.flipX = true;
        }
    }

    public void Hit(int val){
        hitpoints-=val;
        anim.Play("TrashGuy1_hit");
        if(hitpoints<=0){
            KillEnemy();
        }

    }
    private void KillEnemy(){

        Destroy(this.gameObject);
    }
}
