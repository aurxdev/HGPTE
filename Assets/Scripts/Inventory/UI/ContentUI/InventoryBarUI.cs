using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryBarUI : MonoBehaviour
{
    [SerializeField]
    public GameObject content;
    public GameObject inventorySlotPrefab;
    public Player player;
    void Start()
    {
        player.inventory.onInventoryChanged += UpdateUI; // on ajoute l'event
        UpdateUI(); 
    }

    void OnDestroy()
    {
        player.inventory.onInventoryChanged -= UpdateUI;
    }

        public void UpdateUI(){
            if(content.transform.childCount > 0){
                foreach(Transform child in content.transform)
                {
                    Destroy(child.gameObject);
                }
            }
            List <Slot> slots = player.inventory.slots;
            for (int i = 0; i < 6; i++)
            {
                if (slots[i].type == ItemType.NONE)
                {
                    GameObject slotUi = Instantiate(inventorySlotPrefab);
                    slotUi.transform.GetChild(0).gameObject.SetActive(false);
                    slotUi.transform.GetChild(1).gameObject.SetActive(false);
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
}
