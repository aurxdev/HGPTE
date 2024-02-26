using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class MovementManager : MonoBehaviour
{
    public char LastDirection { get; set; }

    private Animator animator;

    private bool isTeleporting = false;

    public bool IsTeleporting
    {
        get => isTeleporting;
        set
        {
            isTeleporting = value;
            if (value && animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    private bool isDashing = false;

    public bool IsDashing
    {
        get => isDashing;
        set
        {
            isDashing = value;
            if (value && animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }


    

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public bool IsOnAnimation() {
        return IsDashing || IsTeleporting;
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
