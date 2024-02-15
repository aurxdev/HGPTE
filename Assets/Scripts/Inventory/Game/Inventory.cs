using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory{

    public List<Slot> slots = new List<Slot>();
    // observer
    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChanged;

    public Inventory(int size){
        for (int i = 0; i < size; i++){
            slots.Add(new Slot());
        }
    }

    public string toString(){
        string msg="";
        for(int i=0; i<slots.Count;i++){
            msg+=slots[i].name;
        }
        return msg;
    }

    //scriptable object

    public void Add(Collectable c){
        // on parcours une premiere fois l'inventaire pour trouver un slot si les 2 ont le meme ID
        foreach(Slot slot in slots){
            if (slot.id == c.item.id && slot.CanAddItem()){
                slot.AddItem(c);
                // on declenche l'event
                onInventoryChanged?.Invoke();
                return;
            }
        }
        // sinon si on met au prochain slot vide
        foreach(Slot slot in slots){
            if (slot.type == ItemType.NONE){
                slot.AddItem(c);
                // on declenche l'event
                onInventoryChanged?.Invoke();
                return;
            }
        }
    }
}