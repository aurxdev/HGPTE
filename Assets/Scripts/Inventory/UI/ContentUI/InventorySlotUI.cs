using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField]
    public Player player;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && transform.childCount == 0)
        {
            DraggableItem draggedItem = dropped.GetComponent<DraggableItem>();
            if(draggedItem != null){
                draggedItem.parentAfterDrag = transform;
                // Debug.Log(player.inventory.toString());
                // player.inventory.SwapItems(draggedItem.id, transform.GetComponent<DraggableItem>().id);
            }
        }

        else if(transform.childCount == 1) {
            Transform existingItem = transform.GetChild(0);
            DraggableItem draggedItem = dropped.GetComponent<DraggableItem>();
            if(draggedItem != null){
                existingItem.SetParent(draggedItem.parentAfterDrag);
                draggedItem.parentAfterDrag = transform;
                // player.inventory.SwapItems(draggedItem.id, existingItem.GetComponent<DraggableItem>().id);
            }
        }
    }
}
