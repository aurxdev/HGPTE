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

    public static readonly int idSpriteDown = 0;

    public static readonly int idSpriteUp = 19;

    public static readonly int idSpriteLeft = 27;

    public static readonly int idSpriteRight = 8;

    public bool IsTeleporting
    {
        get => isTeleporting;
        set
        {
            isTeleporting = value;
        }
    }

    private bool isDashing = false;

    public bool IsDashing
    {
        get => isDashing;
        set
        {
            isDashing = value;
            if (animator != null)
            {
                if (value)
                {
                    animator.SetBool("isDashing", true);
                }
                else
                {
                    animator.SetBool("isDashing", false);
                }
            }
        }
    }


    private bool isWalking = false;
    public bool IsWalking
    {
        get => isWalking;
        set {
            isWalking = value;
            if (animator != null)
            {
                if (value)
                {
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }
            }
        }
    }

    private bool isRunning = false;
    public bool IsRunning {get; set;}


    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("vertical", -1);
        animator.SetFloat("horizontal", 0);
    }

    
    public bool IsOnAnimation()
    {
        return IsDashing || IsTeleporting;
    }

}
