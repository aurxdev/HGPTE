using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot{
    public ItemType type;
    public int count; 
    public int maxItems; 
    public Sprite icon;
    public string name; 
    public int id;

    public Slot(){
        type = ItemType.NONE;
        count = 0;
        maxItems = 64;
    }

    public bool CanAddItem(){
        return count < maxItems;
    }

    public void AddItem(Collectable c){
        this.id=c.item.id;
        this.type = c.item.type;
        this.icon = c.item.imageInventory;
        this.name = c.item.itemName;
        this.count++;
    }
}