using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryBarHUI : MonoBehaviour
{
    [SerializeField]
    public Color selectedColor;
    [SerializeField]
    public GameObject content;
    public GameObject inventorySlotPrefab;
    public Player player;

    void Start()
    {
        player.inventory.onInventoryChanged += UpdateUI; // on ajoute l'event
        player.onSlotChanged += UpdateUI;
        UpdateUI(); 
    }

    void OnDestroy()
    {
        player.inventory.onInventoryChanged -= UpdateUI;
        player.onSlotChanged -= UpdateUI;
    }

    // met à jour l'UI de la barre d'inventaire horizontale
    public void UpdateUI(){
        GameObject slotPrefab;
        if(content.transform.childCount > 0){
            foreach(Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
        }
        List <Slot> slots = player.inventory.slots;
        for (int i = 0; i < 6; i++)
        {
            slotPrefab = Instantiate(inventorySlotPrefab);
            if (slots[i].type == ItemType.NONE || slots[i] == null)
            {
                slotPrefab.transform.GetChild(0).gameObject.SetActive(false);
                slotPrefab.transform.GetChild(1).gameObject.SetActive(false);
                slotPrefab.transform.GetChild(2).gameObject.SetActive(false);
                slotPrefab.transform.SetParent(content.transform, false);
            }
            else
            {
                slotPrefab.transform.GetChild(0).gameObject.GetComponent<Text>().text = slots[i].name;
                slotPrefab.transform.GetChild(1).gameObject.GetComponent<Text>().text = slots[i].count.ToString();
                slotPrefab.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = slots[i].icon;
                slotPrefab.transform.SetParent(content.transform, false);
            }
            if(player.selectedSlot == i){
                slotPrefab.GetComponent<Image>().color = selectedColor;
            }
        }     
    } // UpdateUI()
}
