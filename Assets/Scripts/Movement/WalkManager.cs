using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkManager : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 2;

    [SerializeField]
    private float runSpeed = 5;

    private float speed;

    private float abs(float value) {
        if (value < 0) {
            return -value;
        }
        return value;
    }
    
    // Animates the movement of the player character based on the given direction
    private void AnimateMovement(Vector2 direction, Animator animator)
    {
        if (direction != Vector2.zero && direction.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("vertical", direction.y);
            animator.SetFloat("horizontal", direction.x);
        }
    }

    private void Walk(Animator animator, Rigidbody2D rigidbody2D, MovementManager movement) {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (abs(horizontal) > abs(vertical)) {
            vertical = 0;
        } else {
            horizontal = 0;
        }

        switch (horizontal)
        {
            case 1:
                movement.LastDirection = 'E';
                break;
            case -1:
                movement.LastDirection = 'W';
                break;
            default:
                switch (vertical)
                {
                    case 1:
                        movement.LastDirection = 'N';
                        break;
                    case -1:
                        movement.LastDirection = 'S';
                        break;
                    default:
                    break;
                }
                break;
        }

        if (horizontal == 0 && vertical == 0) {
            animator.SetBool("isMoving", false);
            rigidbody2D.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = new Vector2(horizontal, vertical);
        AnimateMovement(direction, animator);
        rigidbody2D.velocity = new Vector2(horizontal * speed * Time.timeScale, vertical* speed * Time.timeScale);

    }


    void Start()
    {
        speed = walkSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            speed = runSpeed;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            speed = walkSpeed;
        }
    }


    void FixedUpdate()
    {
        Animator visualAnimator = GetComponentInChildren<Animator>();
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        MovementManager movement = GetComponent<MovementManager>();
        if (visualAnimator != null && rigidbody2D != null && movement != null && !movement.IsOnAnimation)
        {
            Walk(visualAnimator, GetComponent<Rigidbody2D>(), GetComponent<MovementManager>());
        }
    }
}
