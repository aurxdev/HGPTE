using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickHandler : MonoBehaviour, IPointerClickHandler
{
    private Player player;
    private Transform informations;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            informations = gameObject.transform.parent.parent.parent.parent;
            player = FindObjectOfType<Player>();
            if(informations != null && player != null && player.IsOpening){
                DraggableItem dg = informations.GetComponent<DraggableItem>();
                player.chestInventory.Add(player.inventory.slots, dg.nbSlot);
                player.inventory.Remove(dg.nbSlot);
                // Destroy(informations.gameObject);

                Debug.Log(player.chestInventory.NumberOfItems());
            }
        }
    }
}