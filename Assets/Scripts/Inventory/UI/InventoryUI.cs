using System;
using System.Collections;
using System.Collections.Generic;
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
            Transform childTransform = inventoryPanel.transform.GetChild(0).GetChild(0);
            Debug.Log(childTransform.name);
        }
    }
    

    public void UpdateUI(Boolean show){

    }

}
