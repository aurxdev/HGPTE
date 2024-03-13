using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkManager : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 2;

    [SerializeField]
    private float runSpeed = 5;

    [SerializeField]
    private float runStaminaCost = 15;


    private float speed;

    private float Abs(float value) {
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
            animator.SetFloat("vertical", direction.y);
            animator.SetFloat("horizontal", direction.x);
        }
    }

    private void Walk(Animator animator, Rigidbody2D rigidbody2D, MovementManager movement) {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Abs(horizontal) > Abs(vertical)) {
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
            rigidbody2D.velocity = Vector2.zero;
            movement.IsWalking = false;
            return;
        }

        movement.IsWalking = true;
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
        Player player = GetComponent<Player>();
        MovementManager movementManager = GetComponent<MovementManager>();

        if (Input.GetKey(KeyCode.LeftShift) && player.GetStamina() > 1 && movementManager != null && movementManager.IsWalking && !movementManager.IsOnAnimation()){
            player.RemoveStamina(runStaminaCost * Time.deltaTime, false);
            speed = runSpeed;
            GetComponentInChildren<Animator>().speed = 1.35f * (runSpeed / 5);     
            movementManager.IsRunning = true;
        } else {
            speed = walkSpeed;
            GetComponentInChildren<Animator>().speed = 1f;
            movementManager.IsRunning = false;
        }
    }


    void FixedUpdate()
    {
        Animator visualAnimator = GetComponentInChildren<Animator>();
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        MovementManager movement = GetComponent<MovementManager>();
        if (visualAnimator != null && rigidbody2D != null && movement != null && !movement.IsOnAnimation())
        {
            Walk(visualAnimator, GetComponent<Rigidbody2D>(), GetComponent<MovementManager>());
        }
    }
}
