using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class MovementManager : MonoBehaviour
{

    private char lastDirection;
    public char LastDirection
    {
        get => lastDirection;
        set
        {
            lastDirection = value;
            if (animator != null)
            {
                GameObject detector = GameObject.Find("Detector");
                switch (value)
                {
                    case 'N':
                        animator.SetFloat("vertical", 1);
                        animator.SetFloat("horizontal", 0);
                        detector.transform.localPosition = new Vector2(0, 0.6f);
                        break;
                    case 'S':
                        animator.SetFloat("vertical", -1);
                        animator.SetFloat("horizontal", 0);
                        detector.transform.localPosition = new Vector2(0, -0.6f);
                        break;
                    case 'E':
                        animator.SetFloat("vertical", 0);
                        animator.SetFloat("horizontal", 1);
                        detector.transform.localPosition = new Vector2(0.6f, 0);
                        break;
                    case 'W':
                        animator.SetFloat("vertical", 0);
                        animator.SetFloat("horizontal", -1);
                        detector.transform.localPosition = new Vector2(-0.6f, 0);
                        break;
                    default:
                        break;
                }
            }
        }
    }

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

    private bool isStuck = false;

    public bool IsStuck
    {
        get => isStuck;
        set
        {
            isStuck = value;
            if (value && animator != null)
            {
                IsWalking = false;
                IsRunning = false;
                IsDashing = false;
                Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
                rigidbody2D.velocity = Vector2.zero;
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

    public bool IsRunning {get; set;}


    private bool isAttacking = false;

    public bool IsAttacking
    {
        get => isAttacking;
        set
        {
            isAttacking = value;
            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            if (animator != null)
            {
                if (value)
                {
                    IsWalking = false;
                    IsRunning = false;
                    rigidbody2D.velocity = Vector2.zero;
                    animator.SetBool("isAttacking", true);
                }
                else
                {
                    animator.SetBool("isAttacking", false);
                }
            }
        }
    }


    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        LastDirection = 'S';
    }

    
    public bool IsOnAnimation()
    {
        Player player = GetComponent<Player>();
        return IsDashing || IsTeleporting || IsStuck || IsAttacking || player.IsPausing;
    }

}
