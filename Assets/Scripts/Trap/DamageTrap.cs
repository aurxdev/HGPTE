using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class DamageTrap : Trap
{
    private Player player;

    [SerializeField]
    private float damage = 20f;

    private bool VerifySprite()
    {
        return GetComponent<SpriteRenderer>().sprite.name == "Spike Trap_7" || GetComponent<SpriteRenderer>().sprite.name == "Spike Trap_9";
    }

    public override void Action(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
    }

    public override void Leave(Collider2D collision)
    {
        player = null;
    }


    public override void Update()
    {
        if (player != null && VerifySprite())
        {
            player.RemoveHp((float)(20 / 13.5) * damage  * Time.deltaTime, false);
        }
    }
}