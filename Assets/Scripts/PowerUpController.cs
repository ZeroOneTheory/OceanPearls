using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.down * 4f * Time.deltaTime, Space.World);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("Hit");
        if (col.gameObject.tag == "Player")
        {
            //Do Power up stuff
            Destroy(this.gameObject);
        }

    }
}
