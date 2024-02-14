using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashManager : MonoBehaviour
{
    private bool hasToDash;

    [SerializeField]
    private float dashDistance = 2.5f; // Distance covered by the player character during a dash

    [SerializeField]
    private float dashDuration = 0.1f; // Duration of the dash animation in seconds

    private float elapsedTime; // Time elapsed since the start of the dash

    void Start()
    {
        hasToDash = false;
        elapsedTime = 0f;
    }

    
    private void Dash(MovementManager movementManager, Rigidbody2D rigidbody2D, Animator animator) {
        movementManager.IsOnAnimation = true;
        Vector2 direction = Vector2.zero;
        switch (movementManager.LastDirection)
        {
            case 'N':
                direction = Vector2.up;
                break;
            case 'S':
                direction = Vector2.down;
                break;
            case 'E':
                direction = Vector2.right;
                break;
            case 'W':
                direction = Vector2.left;
                break;
            default:
                break;
        }

        if (direction != Vector2.zero)
        {
            rigidbody2D.velocity = direction * dashDistance / dashDuration;
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= dashDuration)
            {
                hasToDash = false;
                elapsedTime = 0f;
                rigidbody2D.velocity = Vector2.zero;
                movementManager.IsOnAnimation = false;
            }
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            hasToDash = true;
        }
    }


    void FixedUpdate()
    {
        Animator visualAnimator = GetComponentInChildren<Animator>();
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        MovementManager movement = GetComponent<MovementManager>();
        if (visualAnimator != null && rigidbody2D != null && movement != null)
        {
            if (hasToDash)
            {
                Dash(movement, rigidbody2D, visualAnimator);
            }
        }
    }

}
