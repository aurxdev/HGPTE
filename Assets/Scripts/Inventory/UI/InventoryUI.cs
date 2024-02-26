using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    public GameObject inventoryPanel;
    public GameObject inventorySlotPrefab;

    public GameObject content;

    // instance du joueur
    public Player player;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            if(inventoryPanel.activeSelf) inventoryPanel.transform.LeanMoveLocalX(inventoryPanel.transform.localPosition.x + 500, 0.3f).setEaseOutCubic(); // si on ferme on slide
            else{
                inventoryPanel.transform.LeanMoveLocalX(750, 0.3f).setEaseOutCubic(); // si on ouvre on slide
            }
            inventoryPanel.SetActive(!inventoryPanel.activeSelf); // on active ou desactive
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
                    GameObject slotUi = Instantiate(inventorySlotPrefab);
                    slotUi.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                    slotUi.transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
                    slotUi.transform.GetChild(2).gameObject.SetActive(false);
                    slotUi.transform.SetParent(content.transform, false);
                }
                else
                {
                    GameObject slotUi = Instantiate(inventorySlotPrefab);
                    slotUi.transform.GetChild(0).gameObject.GetComponent<Text>().text = slots[i].name;
                    slotUi.transform.GetChild(1).gameObject.GetComponent<Text>().text = slots[i].count.ToString();
                    slotUi.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = slots[i].icon;
                    slotUi.transform.SetParent(content.transform, false);
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
