using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    private CircleCollider2D attackCollider;

    [SerializeField]
    private float attackRadius = 0.6f;

    [SerializeField]
    private float damage = 10;

    [SerializeField]
    private float attackSpeed = 0.5f;

    private Monster monster;

    // Start is called before the first frame update
    void Start()
    {
        attackCollider = GetComponentInChildren<CircleCollider2D>();
        attackCollider.radius = attackRadius;
    }


    private IEnumerator Attack()
    {
        MovementManager movementManager = GetComponent<MovementManager>();
        movementManager.IsAttacking = true;
        if (monster != null)
        {
            monster.TakeDamage(damage);
        }
        yield return new WaitForSeconds(attackSpeed);
        movementManager.IsAttacking = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            this.monster = monster;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            this.monster = null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        MovementManager movementManager = GetComponent<MovementManager>();
        if (Input.GetButton("Fire1") && movementManager != null && !movementManager.IsAttacking)
        {
            StartCoroutine(Attack());
        }

    }
}
