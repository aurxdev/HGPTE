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
    public string description;

    public Slot(){
        type = ItemType.NONE;
        count = 0;
        maxItems = 64;
    }

    public Slot(int id, ItemType type, int count, int maxItems, Sprite icon, string name, string description){
        this.id = id;
        this.type = type;
        this.count = count;
        this.maxItems = maxItems;
        this.icon = icon;
        this.name = name;
        this.description = description;
    }

    public bool CanAddItem(){
        return count <= maxItems;
    }

    public void AddItem(int id, ItemType type, int count, Sprite icon, string name, string description){
        this.id = id;
        this.type = type;
        this.count=count;
        this.icon = icon;
        this.name = name;
        this.description = description;
    }

    public void AddItem(Collectable c){
        this.id=c.item.id;
        this.type = c.item.type;
        this.icon = c.item.imageInventory;
        this.name = c.item.itemName;
        this.description = c.item.description;
        this.count++;
    }
    public void MoveItem(Slot c){
        this.id=c.id;
        this.type = c.type;
        this.icon = c.icon;
        this.name = c.name;
        this.description = c.description;
        this.count=c.count;
    }
}