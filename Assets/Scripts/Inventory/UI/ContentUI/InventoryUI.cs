using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    public GameObject inventoryPanel;
    [SerializeField]
    public GameObject inventorySlotPrefab;
    [SerializeField]
    public GameObject content;

    [SerializeField]
    private GameObject container;
    [SerializeField]
    private GameObject inventoryInformations;

    // instance du joueur
    [SerializeField]
    public Player player;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            inventoryPanel.SetActive(!inventoryPanel.activeSelf); // on active ou desactive
            if(inventoryPanel.activeSelf) {
                inventoryPanel.transform.LeanMoveLocalX(750, 0.3f).setEaseOutCubic(); // si on ouvre on slide
            } else {
                inventoryPanel.transform.LeanMoveLocalX(inventoryPanel.transform.localPosition.x + 500, 0.1f).setEaseOutCubic(); // si on ferme on slide
            }
            UpdateUI(!inventoryPanel.activeSelf); // on update l'UI
        }
    }
    

    public void UpdateUI(Boolean show){
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

}
