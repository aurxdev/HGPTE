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

    // constructeur
    public Inventory(int size){
        for (int i = 0; i < size; i++){
            slots.Add(new Slot());
        }
    } // Inventory(int)

    public Inventory(List<Slot> list){
        for(int i=0; i<list.Count;i++){
            slots.Add(list[i]);
        }
    } // Inventory(List<Slot>)

    // retourne le nombre d'items dans l'inventaire
    public int NumberOfItems(){
        int count=0;
        foreach(Slot slot in slots){
            if (slot.type != ItemType.NONE){
                count++;
            }
        }
        return count;
    } // NumberOfItems()

    // ajoute un collectable dans l'inventaire
    public void Add(Collectable c)
    {
        AddItemToInventory(c.item.id, c.item.type, -1, c.item.imageInventory, c.item.name, c.item.description);
    } // Add(Collectable)

    // ajoute un item dans l'inventaire
    public void Add(Item item)
    {
        AddItemToInventory(item.data.id, item.data.type, -1, item.data.imageInventory, item.data.name, item.data.description);
    } // Add(Item)

    // ajoute un slot dans l'inventaire
    public void Add(List<Slot> playerSlots, int index)
    {
        AddItemToInventory(playerSlots[index].id, playerSlots[index].type, playerSlots[index].count, playerSlots[index].icon, playerSlots[index].name, playerSlots[index].description);
    } // Add(List<Slot>, int)

    // ajoute un item dans l'inventaire
    private void AddItemToInventory(int id, ItemType type, int count, Sprite icon, string name, string description)
    {
        // on parcours une premiere fois l'inventaire pour trouver un slot si les 2 ont le meme ID
        foreach (Slot slot in slots)
        {
            if (slot.id == id && slot.CanAddItem())
            {
                if (count == -1)count = ++slot.count;
                slot.AddItem(id, type, count, icon, name, description);
                lastSlot = slot;
                // on declenche l'event
                onInventoryChanged?.Invoke();
                onInventoryChangedBar?.Invoke();
                return;
            }
        }
        // sinon si on met au prochain slot vide
        foreach (Slot slot in slots)
        {
            if (slot.type == ItemType.NONE || slot == null)
            {
                if (count == -1)count = ++slot.count;
                slot.AddItem(id, type, count, icon, name, description);
                lastSlot = slot;
                // on declenche l'event
                onInventoryChanged?.Invoke();
                onInventoryChangedBar?.Invoke();
                return;
            }
        }
    } // AddItemToInventory(int, ItemType, int, Sprite, string, string)

    // swap 2 items dans l'inventaire
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
    } // SwapItems(int, int)

    // reset un slot
    public void ResetSlot(Slot s){
        s.type=ItemType.NONE;
        s.count=0;
        s.icon=null;
        s.name="";
        s.id=0;
        s.description="";
    } // ResetSlot(Slot)

    // remove un item de l'inventaire
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
    } // Remove(Collectable)

    // remove un item de l'inventaire
    public void Remove(Item item){
        foreach(Slot slot in slots){
            if (slot.id == item.data.id){
                ResetSlot(slot);
                // on declenche l'event
                onInventoryChanged?.Invoke();
                return;
            }
        }
    } // Remove(Item)

    // contient un item
    public bool Contains(int id, int nb){
        foreach(Slot slot in slots){
            if (slot.id == id && slot.count >= nb){
                return true;
            }
        }
        return false;
    } // Contains(int, int)

    // craft un item
    public bool Craft(int id1, int nb1, int id2, int nb2, ItemData item){
        if (Contains(id1, nb1) && Contains(id2,nb2)){
            RemoveWithID(id1, nb1);
            RemoveWithID(id2, nb2);
            Add(new Item(item));
            return true;
        }
        return false;
    } // Craft(int, int, int, int, ItemData)

    // remove un item de l'inventaire avec un ID
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
    } // RemoveWithID(int, int)

    // remove
    public void Remove(int index){
        ResetSlot(slots[index]);

        // on declenche l'event
        onInventoryChanged?.Invoke();
    } // Remove(int)

    // remove tout
    public void RemoveAll(){
        for(int i=0;i<slots.Count;i++){
            Remove(i);
        }
        
        // on declenche l'event
        onInventoryChanged?.Invoke();
    } // RemoveAll()

}