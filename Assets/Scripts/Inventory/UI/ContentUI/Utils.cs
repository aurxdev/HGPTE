using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Utils : MonoBehaviour
{
        public static void UpdateUI(Boolean show, Player player, GameObject inventorySlotPrefab, GameObject content, GameObject container, GameObject inventoryInformations){
        if (!show)
        {
            List <Slot> slots = player.inventory.slots;
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].type == ItemType.NONE)
                {
                    GameObject slotPrefab = Instantiate(inventorySlotPrefab);
                    Destroy(slotPrefab.transform.GetChild(0).gameObject);
                    slotPrefab.transform.SetParent(content.transform, false);
                }
                else
                {
                    // on crée un slot
                    GameObject slotPrefab = Instantiate(inventorySlotPrefab);
                    GameObject slot = slotPrefab.transform.GetChild(0).gameObject;
                    slot.GetComponent<Image>().sprite = slots[i].icon;
                    slot.GetComponent<DraggableItem>().container = container;
                    slot.GetComponent<DraggableItem>().inventoryInformations = inventoryInformations;
                    slot.GetComponent<DraggableItem>().id = slots[i].id;
                    slot.GetComponent<DraggableItem>().nb = slots[i].count;
                    slot.GetComponent<DraggableItem>().description = slots[i].description;
                    slot.GetComponent<DraggableItem>().iconImage = slots[i].icon;
                    slot.GetComponent<DraggableItem>().nom = slots[i].name;
                    slot.GetComponent<DraggableItem>().nbSlot = i;
                    slotPrefab.transform.SetParent(content.transform, false);
                }
            }
        }
        else
        {
            foreach(Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public static void UpdateSlotsUI(Player player, GameObject inventorySlotPrefab, GameObject content, GameObject container, GameObject inventoryInformations){
        if(content.transform.childCount > 0){
            foreach(Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
        }
        List <Slot> slots = player.inventory.slots;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].type == ItemType.NONE)
            {
                GameObject slotPrefab = Instantiate(inventorySlotPrefab);
                Destroy(slotPrefab.transform.GetChild(0).gameObject);
                slotPrefab.transform.SetParent(content.transform, false);
            }
            else
            {
                // on crée un slot
                GameObject slotPrefab = Instantiate(inventorySlotPrefab);
                GameObject slot = slotPrefab.transform.GetChild(0).gameObject;
                slot.GetComponent<Image>().sprite = slots[i].icon;
                slot.GetComponent<DraggableItem>().container = container;
                slot.GetComponent<DraggableItem>().inventoryInformations = inventoryInformations;
                slot.GetComponent<DraggableItem>().id = slots[i].id;
                slot.GetComponent<DraggableItem>().nb = slots[i].count;
                slot.GetComponent<DraggableItem>().description = slots[i].description;
                slot.GetComponent<DraggableItem>().iconImage = slots[i].icon;
                slot.GetComponent<DraggableItem>().nom = slots[i].name;
                slot.GetComponent<DraggableItem>().nbSlot = i;
                slotPrefab.transform.SetParent(content.transform, false);
            }
        }
    }

}