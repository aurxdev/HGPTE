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
        player.inventory.onInventoryChanged += ShowChestSlotsUI;
    }

    private void OnRectTransformRemoved()
    {
        player.inventory.onInventoryChanged -= ShowChestSlotsUI;
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
        if (isOpen)
        {
            GameObject chestUIClone = GameObject.Find("ChestUI(Clone)");
            if (chestUIClone != null)
            {
                Destroy(chestUIClone);
                isOpen = false;
                player.IsOpening = false;
                player.chestInventory = null;
                if (inventoryUI != null) inventoryUI.maskUI();
            }
        }
        isTrigger=false;
    }

    void Update()
    {
        InventoryUI inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
        if (Input.GetKeyDown(KeyCode.E) && isTrigger && !isOpen)
        {
            ShowChestUI(inventoryUI); return;
        }
        // 
        else if(Input.GetKeyDown(KeyCode.E) && isTrigger && isOpen)
        {
            MaskChestUI(inventoryUI);
        }
    }

    public void ShowChestUI(InventoryUI inventoryUI)
    {
        chestPrefab = Instantiate(chestUI);
        chestPrefab.transform.position = new Vector3(300, 600, 0);
        chestPrefab.transform.SetParent(canvas.transform);
        isOpen = true;
        player.IsOpening = true;
        player.chestInventory = chestSlots;
        ShowChestSlotsUI();
        if (inventoryUI != null && !inventoryUI.inventoryPanel.activeSelf) inventoryUI.showUI();
    }

    public void MaskChestUI(InventoryUI inventoryUI)
    {
        GameObject chestUIClone = GameObject.Find("ChestUI(Clone)");
        if (chestUIClone != null) Destroy(chestUIClone);
        isOpen = false;
        player.IsOpening = false;
        chestSlots = new Inventory(player.chestInventory.slots); // on sauvegarde l'inventaire du coffre
        player.chestInventory = null;
        ShowChestSlotsUI();
        if (inventoryUI != null) inventoryUI.showUI();
    }

    public void ShowChestSlotsUI()
    {
        GameObject slotPrefab;
        GameObject content = chestPrefab.transform.GetChild(0).GetChild(0).gameObject;
        List<Slot> slots = chestSlots.slots;

        Debug.Log(chestSlots.NumberOfItems());

        if(content.transform.childCount > 0){
            foreach(Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
        }
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].type == ItemType.NONE || slots[i] == null)
            {
                slotPrefab = Instantiate(inventorySlotPrefab);
                slotPrefab.transform.GetChild(0).gameObject.SetActive(false);
                slotPrefab.transform.GetChild(1).gameObject.SetActive(false);
                slotPrefab.transform.GetChild(2).gameObject.SetActive(false);
                slotPrefab.transform.SetParent(content.transform, false);
            }
            else
            {
                slotPrefab = Instantiate(inventorySlotPrefab);
                slotPrefab.transform.GetChild(0).gameObject.GetComponent<Text>().text = slots[i].name;
                slotPrefab.transform.GetChild(1).gameObject.GetComponent<Text>().text = slots[i].count.ToString();
                slotPrefab.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = slots[i].icon;
                slotPrefab.transform.SetParent(content.transform, false);
            }
        }
    }
}

