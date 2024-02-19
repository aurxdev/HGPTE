using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class MovementManager : MonoBehaviour
{
    public char LastDirection { get; set; }

    public bool IsDashing { get; set; } = false;

    public bool IsTeleporting { get; set; } = false;

    public bool IsOnAnimation() {
        return IsDashing;
    }

    void Update()
    {
        //If is stuck
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject.transform.position = new Vector2(13.16f, -8.49f);
        }
    }

}
