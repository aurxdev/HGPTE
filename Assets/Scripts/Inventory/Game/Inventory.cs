using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory{

    public List<Slot> slots = new List<Slot>();

    public Inventory(int size){
        for (int i = 0; i < size; i++){
            slots.Add(new Slot());
        }
    }

    //scriptable object

    public void Add(Collectable item){
        foreach(Slot slot in slots){
            if (slot.type == item.type && slot.CanAddItem()){
                slot.AddItem(item);
                return;
            }
        }

        foreach(Slot slot in slots){
            if (slot.type == CollectableType.None){
                slot.AddItem(item);
                return;
            }
        }

    }
}