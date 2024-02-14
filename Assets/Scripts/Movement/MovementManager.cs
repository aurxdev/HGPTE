using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MovementManager : MonoBehaviour
{
    private bool isOnAnimation = false;

    private char lastDirection;

    public char LastDirection
    {
        get { return lastDirection; }
        set { lastDirection = value; }
    }

    public bool IsOnAnimation
    {
        get { return isOnAnimation; }
        set { isOnAnimation = value; }
    }
}
