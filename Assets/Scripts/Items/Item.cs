using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

    public Item(ItemData item)
    {
        this.data = item;
    }
}