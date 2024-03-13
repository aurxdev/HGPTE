using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : Trap
{
    [SerializeField] 
    private float timeStucked = 5f;
    private Animator animator;

    [SerializeField]
    private float timeToWaitBeforeReusing = 10f;

    private float waitBeforeReusing = 0f;

    private MovementManager movementManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator CloseTrap()
    {
        animator.SetBool("Close", true);
        movementManager.IsStuck = true;
        yield return new WaitForSeconds(timeStucked);
        animator.SetBool("Close", false);
        movementManager.IsStuck = false;
    }



    public override void Action(Collider2D collision)
    {
        if (waitBeforeReusing <= 0f)
        {
            movementManager = collision.GetComponent<MovementManager>();
            waitBeforeReusing = timeToWaitBeforeReusing;
            StartCoroutine(CloseTrap());
        }
    }


    public override void Leave(Collider2D collision)
    {
        movementManager = null;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (waitBeforeReusing > 0f)
        {
            waitBeforeReusing -= Time.deltaTime;
            if (waitBeforeReusing <= 0f)
            {
                waitBeforeReusing = 0f;
            }
        }
    }

}
