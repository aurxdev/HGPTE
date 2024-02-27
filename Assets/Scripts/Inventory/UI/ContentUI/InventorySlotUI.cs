using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && transform.childCount == 0)
        {
            DraggableItem draggedItem = dropped.GetComponent<DraggableItem>();
            draggedItem.parentAfterDrag = transform;
        }
        else if(transform.childCount == 1) {
            Transform existingItem = transform.GetChild(0);
            DraggableItem draggedItem = dropped.GetComponent<DraggableItem>();
            existingItem.SetParent(draggedItem.parentAfterDrag);
            draggedItem.parentAfterDrag = transform;
        }

    }
}
