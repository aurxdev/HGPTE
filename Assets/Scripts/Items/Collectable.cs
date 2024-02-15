using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ItemData item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player && item != null && item.isCollectable)
        {
            player.inventory.Add(this);
            Destroy(gameObject);
        }
    }
}

