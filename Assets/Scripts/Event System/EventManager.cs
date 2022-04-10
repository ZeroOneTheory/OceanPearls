using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action ExampleEvent;

    private void Upate()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            /* if(ExampleEvent!=null){
                ExampleEvent();
            } */
            ExampleEvent?.Invoke();
        }

    }
}
