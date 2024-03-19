using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    [SerializeField]
    private float range = 10;

    [SerializeField]
    private float speed = 1;

    private Animator animator;

    private float lastMovementTime = 0;

    [SerializeField]
    private float randomlyMoveTime = 5;

    private bool isMoving = false;

    private Vector2 randomlyVector;

    [SerializeField]
    private GameObject playerObject;


    private float Abs(float value)
    {
        if (value < 0)
        {
            return -value;
        }
        return value;
    }


    private IEnumerator RandomlyMove()
    {
        animator.SetBool("isMoving", true);
        isMoving = true;
        lastMovementTime = randomlyMoveTime* (float) 1.5;
        Rigidbody2D rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();

        
        switch (Random.Range(0, 4))
        {
            case 0:
                rb.velocity = new Vector2(1, 0);
                animator.SetFloat("horizontal", 1);
                animator.SetFloat("vertical", 0);
                break;
            case 1:
                rb.velocity = new Vector2(-1, 0);
                animator.SetFloat("horizontal", -1);
                animator.SetFloat("vertical", 0);
                break;
            case 2:
                rb.velocity = new Vector2(0, 1);
                animator.SetFloat("horizontal", 0);
                animator.SetFloat("vertical", 1);
                break;
            case 3:
                rb.velocity = new Vector2(0, -1);
                animator.SetFloat("horizontal", 0);
                animator.SetFloat("vertical", 1);
                transform.parent.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                break;
        }
        yield return new WaitForSeconds(randomlyMoveTime);
        isMoving = false;
        transform.parent.gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }


    private void Move(Vector2 vector2)
    {
        animator.SetBool("isMoving", true);
        switch (vector2.x)
        {
            case float n when n > 0:
                animator.SetFloat("horizontal", 1);
                break;
            case float n when n < 0:
                animator.SetFloat("horizontal", -1);
                break;
            default:
                animator.SetFloat("horizontal", 0);
                break;
        }

        if (vector2.x < 0) {
            transform.parent.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else if (vector2.x > 0) {
            transform.parent.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        switch (vector2.y)
        {
            case float n when n > 0:
                animator.SetFloat("vertical", 1);
                break;
            case float n when n < 0:
                animator.SetFloat("vertical", -1);
                break;
            default:
                animator.SetFloat("vertical", 0);
                break;
        }
        Vector2 res = new Vector2(vector2.x * speed * Time.deltaTime, vector2.y * speed * Time.deltaTime);
        transform.parent.Translate(res);
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.gameObject.GetComponent<Animator>();
        randomlyVector = new Vector2(0, 0);
    }

    
    private bool PlayerNear()
    {
        if (playerObject != null)
        {
            if (Vector2.Distance(playerObject.transform.position, transform.parent.position) < range)
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastMovementTime > 0) {
            lastMovementTime -= Time.deltaTime;
            if (lastMovementTime <= 0) {
                Rigidbody2D rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(0, 0);
            }
        }

        if (transform.parent != null)
        {
            GameObject parent = transform.parent.gameObject;
            if (PlayerNear())
            {
                if (parent.GetComponent<Monster>().GetPlayer() != null) {
                    animator.SetBool("isMoving", false);
                    return;
                }
                Vector2 direction = playerObject.transform.position - parent.transform.position;
                if (Abs(direction.x) > Abs(direction.y))
                {
                    direction.y = 0;
                } else
                {
                    direction.x = 0;
                }
                if (direction.x != 0) {
                    direction.x = (direction.x > 0) ? 1 : -1;
                }
                if (direction.y != 0) {
                    direction.y = (direction.y > 0) ? 1 : -1;
                }
                Move(direction);
            } else {
                if (!isMoving)
                {
                    animator.SetBool("isMoving", false);
                    if (lastMovementTime <= 0) {
                        StartCoroutine(RandomlyMove());
                    }
                }
            }
        }
    }
}
