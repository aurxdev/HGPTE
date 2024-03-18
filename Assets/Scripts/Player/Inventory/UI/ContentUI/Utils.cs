using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Utils : MonoBehaviour
{
    // reset les informations du panel de l'inventaire
    public static void ResetInformations(GameObject inventoryInformations, Sprite defaultIcon, Player player){
        int nb = player.inventory.NumberOfItems();
        int nbMax = player.inventory.slots.Count;

        inventoryInformations.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = defaultIcon;
        inventoryInformations.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        inventoryInformations.transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
        inventoryInformations.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = nb.ToString() + " / " + nbMax.ToString();
        inventoryInformations.transform.GetChild(3).gameObject.GetComponent<Text>().text = "";
    } // ResetInformations()

    // met à jour l'UI de l'inventaire
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
    } // UpdateUI()

    // met à jour les slots de l'inventaire
    public static void UpdateSlotsUI(Player player, GameObject inventorySlotPrefab, GameObject content, GameObject container, GameObject inventoryInformations, Sprite defaultIcon){
        ResetInformations(inventoryInformations, defaultIcon, player);
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
    } // UpdateSlotsUI()

}