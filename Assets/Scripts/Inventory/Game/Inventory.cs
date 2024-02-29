using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory{

    public List<Slot> slots = new List<Slot>();

    public Collectable lastItem;
    public Slot lastSlot;

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
        lastItem = c;
        // on parcours une premiere fois l'inventaire pour trouver un slot si les 2 ont le meme ID
        foreach(Slot slot in slots){
            if (slot.id == c.item.id && slot.CanAddItem()){
                slot.AddItem(c);
                lastSlot=slot;
                // on declenche l'event
                onInventoryChanged?.Invoke();
                return;
            }
        }
        // sinon si on met au prochain slot vide
        foreach(Slot slot in slots){
            if (slot.type == ItemType.NONE){
                slot.AddItem(c);
                lastSlot=slot;
                // on declenche l'event
                onInventoryChanged?.Invoke();
                return;
            }
        }
        lastItem=null;
    }

    public void SwapItems(int index1, int index2)
    {
        if (index1 < 0 || index1 >= slots.Count || index2 < 0 || index2 >= slots.Count)
        {
            Debug.LogError("Index out of range");
            return;
        }

        Slot temp = slots[index1];
        slots[index1] = slots[index2];
        slots[index2] = temp;

        // on déclenche l'event
        onInventoryChanged?.Invoke();
    }



    public void Remove(int index){
        slots[index]=null;

        // on declenche l'event
        onInventoryChanged?.Invoke();
    }

    public void RemoveAll(){
        for(int i=0;i<slots.Count;i++){
            slots[i]=null;
        }
        
        // on declenche l'event
        onInventoryChanged?.Invoke();
    }

}