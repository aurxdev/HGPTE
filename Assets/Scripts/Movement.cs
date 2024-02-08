using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement of the player character.
/// </summary>
public class Movement : MonoBehaviour
{
    [SerializeField]
    public float walkSpeed; // Speed of the player character when walking

    [SerializeField]
    public float sprintSpeed; // Speed of the player character when sprinting

    [SerializeField]
    public float dashDistance; // Distance covered by the player character during a dash

    [SerializeField]
    float dashDuration = 0.1f; // Duration of the dash animation in seconds

    private float speed; // Current speed of the player character

    public Animator animator; // Animator component for controlling animations

    private float horizontal; // Horizontal input value
    private float vertical; // Vertical input value

    private char lastDirection; // Last movement direction of the player character
    private bool isOnAnimation; // Flag indicating if the player character is currently in an animation






    // Start is called before the first frame update
    void Start()
    {
        horizontal = 0;
        vertical = 0;
        lastDirection = 'S';
        speed = walkSpeed;
        isOnAnimation = false;
    }

    // Animates the movement of the player character based on the given direction
    void AnimateMovement(Vector3 direction)
    {
        if (direction != null)
        {
            if (direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);
                animator.SetFloat("vertical", direction.y);
                animator.SetFloat("horizontal", direction.x);
            }
        }
    }





    // Checks if the player character is stopping its movement
    private bool isStopping(float h, float v)
    {
        return (h > 0 && horizontal > h) ||
               (h < 0 && horizontal < h) ||
               (v > 0 && vertical > v) ||
               (v < 0 && vertical < v);
    }




    // Manages the movement of the player character
    private void MovementManager()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > Mathf.Abs(v))
        {
            v = 0;
        }
        else if (Mathf.Abs(v) > Mathf.Abs(h))
        {
            h = 0;
        }
        if (isStopping(h, v))
        {
            animator.SetBool("isMoving", false);
            return;
        }

        horizontal = h;
        vertical = v;

        if (horizontal > 0) lastDirection = 'E';
        if (horizontal < 0) lastDirection = 'W';
        if (vertical > 0) lastDirection = 'N';
        if (vertical < 0) lastDirection = 'S';

        if ((horizontal > 0 && vertical == 0) || (horizontal == 0 && vertical > 0) || (horizontal < 0 && vertical == 0) || (horizontal == 0 && vertical < 0))
        {
            // Move the player character
            Vector3 direction = new Vector3(h, v);
            // Animate the player character
            AnimateMovement(direction);
            transform.position += direction * speed * Time.deltaTime;
        }
    }




    // Plays the dash animation and moves the player character in the specified direction
    private IEnumerator DashAnimation(Vector3 dashDirection)
    {
        float elapsedTime = 0f;
        animator.SetBool("isMoving", true);

        while (elapsedTime < dashDuration)
        {
            transform.position += dashDirection * (dashDistance / dashDuration) * Time.deltaTime;
            animator.SetFloat("vertical", dashDirection.y);
            animator.SetFloat("horizontal", dashDirection.x);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        animator.SetBool("isMoving", false);
        isOnAnimation = false;
    }




    // Manages the dash action of the player character
    private void DashManager()
    {
        Vector3 dashDirection = Vector3.zero;

        switch (lastDirection)
        {
            case 'S':
                dashDirection = new Vector3(0, -1);
                break;
            case 'N':
                dashDirection = new Vector3(0, 1);
                break;
            case 'E':
                dashDirection = new Vector3(1, 0);
                break;
            case 'W':
                dashDirection = new Vector3(-1, 0);
                break;
        }

        isOnAnimation = true;
        StartCoroutine(DashAnimation(dashDirection));
    }




    void Update()
    {
        if (!isOnAnimation)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && speed != sprintSpeed)
            {
                speed = sprintSpeed;
                animator.SetBool("isRunning", true);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && speed == sprintSpeed)
            {
                speed = walkSpeed;
                animator.SetBool("isRunning", false);
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                DashManager();
            }
        }
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isOnAnimation)
        {
            MovementManager();
        }
    }
}