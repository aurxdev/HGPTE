using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot{
    public CollectableType type; // Type of item in the slot
    public int count; // Number of items in the slot
    public int maxItems; // Maximum number of items in the slot

    public Slot(){
        type = CollectableType.None;
        count = 0;
        maxItems = 64;
    }

    public bool CanAddItem(){
        return count < maxItems;
    }

    public void AddItem(CollectableType type){
        this.type = type;
        count++;
    }
}