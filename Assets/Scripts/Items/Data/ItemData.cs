using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "HGPTE/Item data")]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    public ItemType type;
    public string description;
    public Sprite imageMap;
    public Sprite imageInventory;

    public Boolean isCollectable;


}

public enum ItemType{
    NONE,
    EQUIPMENT,
    FOOD,
    MISC,
    ENEMY,
    MATERIAL,
    POTION,
    WEAPON
}
