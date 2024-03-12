using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickChestHandler : MonoBehaviour, IPointerClickHandler
{
    private Player player;
    private Transform informations;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            informations = gameObject.transform.parent;
            player = FindObjectOfType<Player>();
            if(informations != null && player != null && player.IsOpening){
                DraggableItemChest dg = informations.GetComponent<DraggableItemChest>();
                player.inventory.Add(player.chestInventory.slots, dg.nbSlot);
                player.chestInventory.Remove(dg.nbSlot);
                // Destroy(informations.gameObject);

                Debug.Log(player.chestInventory.NumberOfItems());
            }
        }
    }
}