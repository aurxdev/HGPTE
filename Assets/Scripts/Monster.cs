using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField]
    private float maxHealth = 100;

    [SerializeField]
    private float currentHealth;

    [SerializeField]
    private float damage = 30;

    private bool canAttack = true;

    [SerializeField]
    private float attackSpeed = 1;

    [SerializeField]
    private float attackRadius = 0.6f;

    [SerializeField]
    private CircleCollider2D attackCollider;

    Player player;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player = collider.GetComponent<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player = null;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        attackCollider = GetComponentInChildren<CircleCollider2D>();
    }

    private IEnumerator Attack()
    {
        if (player != null)
        {
            canAttack = false;
            player.RemoveHp(damage, true);
            yield return new WaitForSeconds(attackSpeed);
            canAttack = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        attackCollider.radius = attackRadius;
        if (player != null && canAttack)
        {
            StartCoroutine(Attack());
        }
    }
}
