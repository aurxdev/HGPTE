using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    public GameObject inventoryPanel;
    public GameObject inventorySlotPrefab;
    public Player player;
    public List<SlotUI> slots = new List<SlotUI>();

    void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            UpdateUI(!inventoryPanel.activeSelf);
        }
    }
    

    public void UpdateUI(Boolean show){
        Transform childTransform = inventoryPanel.transform.GetChild(0).GetChild(0);
        if (show)
        {
            List <Slot> slots = player.inventory.slots;
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].type == CollectableType.None)
                {
                }
            }
        }
        else
        {

        }
    }

}
