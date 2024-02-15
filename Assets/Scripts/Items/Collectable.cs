using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableType type;
    public int id;
    public int nb;
    public bool isCollectable = true;
    public Sprite icon;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player && isCollectable)
        {
            player.inventory.Add(this);
            Destroy(gameObject);
        }
    }
}


public enum CollectableType
{
    None,
    HealthPotion,
    ManaPotion,
    Coin
}