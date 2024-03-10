using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
            ShowChestUI();
            if (inventoryUI != null && !inventoryUI.inventoryPanel.activeSelf)inventoryUI.showUI();
        }
        else if(Input.GetKeyDown(KeyCode.E) && isTrigger && isOpen)
        {
            GameObject chestUIClone = GameObject.Find("ChestUI(Clone)");
            if(chestUIClone != null)Destroy(chestUIClone);
            isOpen = false;
            player.IsOpening = false;
            chestSlots = new Inventory(player.chestInventory.slots); // on sauvegarde l'inventaire du coffre
            ShowChestUI();
            if (inventoryUI != null)inventoryUI.showUI();
        }
    }

    public void ShowChestUI()
    {
        GameObject slotPrefab;
        GameObject content = chestPrefab.transform.GetChild(0).GetChild(0).gameObject;
        List<Slot> slots = player.chestInventory.slots;
        Debug.Log(player.chestInventory.NumberOfItems());

        if(content.transform.childCount > 0){
            foreach(Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
        }
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].type == ItemType.NONE)
            {
                slotPrefab = Instantiate(inventorySlotPrefab);
                Destroy(slotPrefab.transform.GetChild(0).gameObject);
                slotPrefab.transform.SetParent(content.transform, false);
            }
            else
            {
                slotPrefab = Instantiate(inventorySlotPrefab);
                GameObject slot = slotPrefab.transform.GetChild(0).gameObject;
                Debug.Log(slots[i].name);
                slot.GetComponent<Image>().sprite = slots[i].icon;
                slot.GetComponent<DraggableItem>().container = canvas;
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

