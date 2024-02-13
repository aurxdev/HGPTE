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

    public void Add(CollectableType type){
        foreach(Slot slot in slots){
            if (slot.type == type && slot.CanAddItem()){
                slot.AddItem(type);
                return;
            }
        }

        foreach(Slot slot in slots){
            if (slot.type == CollectableType.None){
                slot.AddItem(type);
                return;
            }
        }

    }
}