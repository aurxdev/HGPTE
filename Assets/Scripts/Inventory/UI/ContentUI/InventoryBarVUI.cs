using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryBarVUI : MonoBehaviour, IDropHandler
{
    [SerializeField]
    public Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && transform.childCount == 0)
        {
            DraggableItem draggedItem = dropped.GetComponent<DraggableItem>();
            if(draggedItem != null){
                draggedItem.parentAfterDrag = transform;

                Debug.Log(player.inventory.toString());
                player.inventory.SwapItems(draggedItem.nbSlot, -1);
            }
        }

        else if(transform.childCount == 1) {
            Transform existingItem = transform.GetChild(0);
            DraggableItem draggedItem = dropped.GetComponent<DraggableItem>();
            if(draggedItem != null){
                existingItem.SetParent(draggedItem.parentAfterDrag);
                draggedItem.parentAfterDrag = transform;


                // Debug.Log("existant: "+existingItem.GetComponent<DraggableItem>().nom);
                // Debug.Log("dragged: "+draggedItem.nom);
                player.inventory.SwapItems(existingItem.GetComponent<DraggableItem>().nbSlot,draggedItem.nbSlot);
            }
        }
    }
}
