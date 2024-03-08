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
    //
    public delegate void OnInventoryChangedBar();
    public event OnInventoryChangedBar onInventoryChangedBar;

    public Inventory(int size){
        for (int i = 0; i < size; i++){
            slots.Add(new Slot());
        }
    }

    public string toString(){
        string msg="";
        for(int i=0; i<slots.Count;i++){
            msg+= slots[i].count.ToString() + ' ' + slots[i].name+'\n';
        }
        return msg;
    }

    public void Add(Collectable c){
        lastItem = c;
        // on parcours une premiere fois l'inventaire pour trouver un slot si les 2 ont le meme ID
        foreach(Slot slot in slots){
            if (slot.id == c.item.id && slot.CanAddItem()){
                slot.AddItem(c);
                lastSlot=slot;
                // on declenche l'event
                onInventoryChanged?.Invoke();
                onInventoryChangedBar?.Invoke();
                return;
            }
        }
        // sinon si on met au prochain slot vide
        foreach(Slot slot in slots){
            if (slot.type == ItemType.NONE || slot == null){
                slot.AddItem(c);
                lastSlot=slot;
                // on declenche l'event
                onInventoryChanged?.Invoke();
                onInventoryChangedBar?.Invoke();
                return;
            }
        }
        lastItem=null;
    }

    public void SwapItems(int index1, int index2)
    {

        if(index2 == -1)
        {
            Slot tmp = new Slot(slots[index1].id, slots[index1].type, slots[index1].count, slots[index1].maxItems, slots[index1].icon, slots[index1].name, slots[index1].description);
            Remove(index1);
            
            // vérifie si un slot vide est disponible
            Slot emptySlot = slots.Find(slot => slot.type == ItemType.NONE);
            if (emptySlot == null)
            {
                Debug.LogError("Erreur swap: Pas de slot vide disponible");
                return;
            }

            emptySlot.MoveItem(tmp);
            lastSlot = emptySlot;

            // on déclenche l'event
            onInventoryChangedBar?.Invoke();
            return;
        }

        // vérifie si les index sont valides
        if (index1 < 0 || index1 >= slots.Count || index2 < 0 || index2 >= slots.Count)
        {
            Debug.LogError("Erreur swap: Index invalide");
            return;
        }

        // sinon on swap

        Slot temp = slots[index1];
        slots[index1] = slots[index2];
        slots[index2] = temp;

        // on déclenche l'event
        onInventoryChangedBar?.Invoke();
    }

    public void Remove(int index){
        slots[index].type=ItemType.NONE;
        slots[index].count=0;
        slots[index].icon=null;
        slots[index].name="";
        slots[index].id=0;
        slots[index].description="";

        // on declenche l'event
        onInventoryChangedBar?.Invoke();
    }

    public void RemoveAll(){
        for(int i=0;i<slots.Count;i++){
            Remove(i);
        }
        
        // on declenche l'event
        onInventoryChangedBar?.Invoke();
    }

}