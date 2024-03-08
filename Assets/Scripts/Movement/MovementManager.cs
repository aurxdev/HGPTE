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

    private Sprite[] sprites;

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
            if (value && animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        sprites = Resources.LoadAll<Sprite>("Graphics/Images/Sprites/Character/character");
    }


    public void setSprite(int id) {
        if (id >= 0 && id < sprites.Length) {
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[id];
        }
    }

    

    public bool IsOnAnimation()
    {
        return IsDashing || IsTeleporting;
    }

}
