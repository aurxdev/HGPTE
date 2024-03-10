using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChestContainer : MonoBehaviour
{
    [SerializeField]
    private int capacity;
    [SerializeField]
    private Inventory chestSlots;
    [SerializeField]
    private int distance;
    [SerializeField]
    private GameObject chestUI;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject inventorySlotPrefab;
    private bool isTrigger;
    private bool isOpen;
    private Player player;
    private GameObject chestPrefab;

    void Start(){
        chestSlots = new Inventory(capacity); // on initialise l'inventaire
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (player)
        {
            isTrigger = true;
            isOpen=false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InventoryUI inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
        GameObject chestUIClone = GameObject.Find("ChestUI(Clone)");
        if(isOpen && chestUIClone != null){
            Destroy(chestUIClone);
            isOpen = false;
            player.IsOpening = false;
            player.chestInventory = null;
            if (inventoryUI != null)inventoryUI.maskUI();
        }
        isTrigger=false;
    }

    void Update()
    {
        InventoryUI inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
        if (Input.GetKeyDown(KeyCode.E) && isTrigger && !isOpen)
        {
            chestPrefab = Instantiate(chestUI);
            chestPrefab.transform.position = new Vector3(300, 500, 0);
            chestPrefab.transform.SetParent(canvas.transform);
            isOpen = true;
            player.IsOpening = true;
            player.chestInventory = chestSlots;
            ShowChestUI(false);
            if (inventoryUI != null && !inventoryUI.inventoryPanel.activeSelf)inventoryUI.showUI();
        }
        else if(Input.GetKeyDown(KeyCode.E) && isTrigger && isOpen)
        {
            GameObject chestUIClone = GameObject.Find("ChestUI(Clone)");
            if(chestUIClone != null)Destroy(chestUIClone);
            isOpen = false;
            player.IsOpening = false;
            player.chestInventory = null;
            ShowChestUI(true);
            if (inventoryUI != null)inventoryUI.showUI();
        }
    }

    public void ShowChestUI(bool show)
    {
        GameObject slotPrefab;
        GameObject content = chestPrefab.transform.GetChild(0).GetChild(0).gameObject;
        Debug.Log(player.chestInventory.toString());
        if (!show)
        {
            for (int i = 0; i < chestSlots.slots.Count; i++)
            {
                if (chestSlots.slots[i].type == ItemType.NONE)
                {
                    slotPrefab = Instantiate(inventorySlotPrefab);
                    Destroy(slotPrefab.transform.GetChild(0).gameObject);
                    slotPrefab.transform.SetParent(content.transform, false);
                }
                else
                {
                    slotPrefab = Instantiate(inventorySlotPrefab);
                    GameObject slot = slotPrefab.transform.GetChild(0).gameObject;
                    Debug.Log(chestSlots.slots[i].name);
                    slot.GetComponent<Image>().sprite = chestSlots.slots[i].icon;
                    slot.GetComponent<DraggableItem>().container = canvas;
                    slot.GetComponent<DraggableItem>().id = chestSlots.slots[i].id;
                    slot.GetComponent<DraggableItem>().nb = chestSlots.slots[i].count;
                    slot.GetComponent<DraggableItem>().description = chestSlots.slots[i].description;
                    slot.GetComponent<DraggableItem>().iconImage = chestSlots.slots[i].icon;
                    slot.GetComponent<DraggableItem>().nom = chestSlots.slots[i].name;
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
}
