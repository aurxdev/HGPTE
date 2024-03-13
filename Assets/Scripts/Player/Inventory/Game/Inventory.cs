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

    public Inventory(List<Slot> list){
        for(int i=0; i<list.Count;i++){
            slots.Add(list[i]);
        }
    }

    public int NumberOfItems(){
        int count=0;
        foreach(Slot slot in slots){
            if (slot.type != ItemType.NONE){
                count++;
            }
        }
        return count;
    }

    // to do: refactor this method
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

    public void Add(Item item){
        // on parcours une premiere fois l'inventaire pour trouver un slot si les 2 ont le meme ID
        foreach(Slot slot in slots){
            if (slot.id == item.data.id && slot.CanAddItem()){
                slot.AddItem(item);
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
                slot.AddItem(item);
                lastSlot=slot;
                // on declenche l'event
                onInventoryChanged?.Invoke();
                onInventoryChangedBar?.Invoke();
                return;
            }
        }
    }

    public void Add(List<Slot> playerSlots, int index){
        // on parcours une premiere fois l'inventaire pour trouver un slot si les 2 ont le meme ID
        foreach(Slot slot in slots){
            if (slot.id == playerSlots[index].id && slot.CanAddItem()){
                slot.AddItem(playerSlots[index].id, playerSlots[index].type, playerSlots[index].count, playerSlots[index].icon, playerSlots[index].name, playerSlots[index].description);
                // on declenche l'event
                onInventoryChanged?.Invoke();
                return;
            }
        }
        // sinon si on met au prochain slot vide
        foreach(Slot slot in slots){
            if (slot.type == ItemType.NONE || slot == null){
                slot.AddItem(playerSlots[index].id, playerSlots[index].type, playerSlots[index].count, playerSlots[index].icon, playerSlots[index].name, playerSlots[index].description);
                // on declenche l'event
                onInventoryChanged?.Invoke();
                return;
            }
        }
    
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
            onInventoryChanged?.Invoke();
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
        onInventoryChanged?.Invoke();
    }

    public void ResetSlot(Slot s){
        s.type=ItemType.NONE;
        s.count=0;
        s.icon=null;
        s.name="";
        s.id=0;
        s.description="";
    }

    public void Remove(Collectable c){
        foreach(Slot slot in slots){
            if (slot.id == c.item.id){
                ResetSlot(slot);

                lastSlot=slot;
                // on declenche l'event
                onInventoryChanged?.Invoke();
                return;
            }
        }
    }

    // inventaire qui contient un item
    public bool Contains(int id, int nb){
        foreach(Slot slot in slots){
            if (slot.id == id && slot.count >= nb){
                return true;
            }
        }
        return false;
    }

    public bool Craft(int id1, int nb1, int id2, int nb2, ItemData item){
        if (Contains(id1, nb1) && Contains(id2,nb2)){
            RemoveWithID(id1, nb1);
            RemoveWithID(id2, nb2);
            Add(new Item(item));
            return true;
        }
        return false;
    }

    public void RemoveWithID(int id, int nb){
        foreach(Slot slot in slots){
            if (slot.id == id && slot.count == nb){
                ResetSlot(slot);
                // on declenche l'event
                onInventoryChanged?.Invoke();
                return;
            }
            else if(slot.id == id && slot.count > nb){
                slot.count -= nb;
                // on declenche l'event
                onInventoryChanged?.Invoke();
                return;

            }
        }
    }

    // remove avec la place dans l'inventaire
    public void Remove(int index){
        ResetSlot(slots[index]);

        // on declenche l'event
        onInventoryChanged?.Invoke();
    }

    public void RemoveAll(){
        for(int i=0;i<slots.Count;i++){
            Remove(i);
        }
        
        // on declenche l'event
        onInventoryChanged?.Invoke();
    }

}