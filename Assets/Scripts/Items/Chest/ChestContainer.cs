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

    // appelé avant la première frame
    void Start(){
        chestSlots = new Inventory(capacity); // on initialise l'inventaire
        player = GameObject.FindObjectOfType<Player>();
        player.chestInventory = chestSlots;
        player.chestInventory.onInventoryChanged += ShowChestSlotsUI;
    } // Start()

    // appelé lors de la destruction
    void Destroy()
    {
        player.chestInventory.onInventoryChanged -= ShowChestSlotsUI;
    } // Destroy()

    // lorsque l'inventaire du coffre change
    private void ChestInventory_onInventoryChanged()
    {
        throw new System.NotImplementedException();
    } // ChestInventory_onInventoryChanged()

    // lorsque le joueur entre dans la zone du coffre
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            isTrigger = true;
            isOpen=false;
        }

    } // OnTriggerEnter2D(Collider2D)

    // lorsque le joueur sort de la zone du coffre
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))return;
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
                // attention cette ligne bug
                if (inventoryUI != null && inventoryUI.inventoryPanel.activeSelf) inventoryUI.maskUI();
            }
        }
        isTrigger=false;
    } // OnTriggerExit2D(Collider2D)

    // mise à jour de l'inventaire du coffre
    void Update()
    {
        InventoryUI inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
        if (Input.GetKeyDown(KeyCode.E) && isTrigger && !isOpen)
        {
            ShowChestUI(inventoryUI); return;
        }
        // else if((Input.GetKeyDown(KeyCode.Tab) && isTrigger && isOpen)){
        //     MaskChestUI(inventoryUI);
        //     inventoryUI.inventoryPanel.SetActive(false);
        // }
        else if((Input.GetKeyDown(KeyCode.E) && isTrigger && isOpen))
        {
            MaskChestUI(inventoryUI);
        }
    } // Update()

    // affichage de l'interface du coffre
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
    } // ShowChestUI(InventoryUI)

    // masquage de l'interface du coffre
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
    } // MaskChestUI(InventoryUI)

    // affichage des slots de l'inventaire du coffre
    public void ShowChestSlotsUI()
    {
        if (!isOpen) return;
        GameObject slotPrefab;
        GameObject content = chestPrefab.transform.GetChild(0).GetChild(0).gameObject;
        List<Slot> slots = player.chestInventory.slots;

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
                slotPrefab.transform.GetChild(0).gameObject.SetActive(false);
                slotPrefab.transform.GetChild(1).gameObject.SetActive(false);
                slotPrefab.transform.GetChild(2).gameObject.SetActive(false);
                slotPrefab.transform.GetChild(3).gameObject.SetActive(false);
                slotPrefab.transform.SetParent(content.transform, false);
            }
            else
            {
                slotPrefab = Instantiate(inventorySlotPrefab);
                slotPrefab.transform.GetChild(0).gameObject.GetComponent<Text>().text = slots[i].name;
                slotPrefab.transform.GetChild(1).gameObject.GetComponent<Text>().text = slots[i].count.ToString();
                slotPrefab.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = slots[i].icon;
                slotPrefab.AddComponent<DraggableItemChest>();
                slotPrefab.GetComponent<DraggableItemChest>().nbSlot = i;
                slotPrefab.transform.SetParent(content.transform, false);
            }
        }
    }// ShowChestSlotsUI()
}

